using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Erwine.Leonard.T.Toolkit.Tracing
{
    public class InMemoryTraceListenener : TraceListener
    {
        private TraceInformation _categorizedInformation = null;
        private StringBuilder _categorizedMessage = new StringBuilder();
        private char[] _currentLine = new char[0];
        private char[] _currentIndent = new char[0];
        private Collection<TraceInformation> _traceData = new Collection<TraceInformation>();

        public override bool IsThreadSafe { get { return true; } }

        public InMemoryTraceListenener() : base("InMemoryTraceListener") { }

        public void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, TraceInformation data)
        {
            if (data == null)
                data = new TraceInformation();

            if (eventCache == null)
                eventCache = new TraceEventCache();

            data.DateTime = eventCache.DateTime;
            data.Timestamp = eventCache.Timestamp;
            data.Callstack = eventCache.Callstack;
            data.LogicalOperationStack = (eventCache.LogicalOperationStack == null) ? new string[0] : eventCache.LogicalOperationStack.ToArray().Select(o => (o == null) ? "" : o.ToString()).ToArray();
            data.ProcessId = eventCache.ProcessId;
            data.ThreadId = eventCache.ThreadId;
            data.Source = (String.IsNullOrEmpty(source)) ? Properties.Settings.Default.TraceSource_Name : source;
            data.EventType = eventType;
            if (data.Message == null)
                data.Message = "";
            if (data.DetailMessage == null)
                data.DetailMessage = "";
            if (data.Category == null)
                data.Category = Properties.Settings.Default.Category_Event;
            if (data.ActivityId.HasValue)
                data.ActivityId = Trace.CorrelationManager.ActivityId;

            lock (this._traceData)
            {
                this.FlushCategorizedInformation();
                this._traceData.Add(data);
            }
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            this.TraceData(eventCache, source, eventType, id, TraceInformation.Create(data));
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            this.TraceData(eventCache, source, eventType, id, TraceInformation.Create(data));
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            this.TraceData(eventCache, source, eventType, id, new TraceInformation());
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            this.TraceData(eventCache, source, eventType, id, new TraceInformation { Message = message });
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            this.TraceEvent(eventCache, source, eventType, id, String.Format(format, args));
        }

        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            this.TraceData(eventCache, source, TraceEventType.Transfer, id, new TraceInformation { Message = message, ActivityId = relatedActivityId });
        }

        public override void Fail(string message)
        {
            this.Fail(message, null);
        }

        public override void Fail(string message, string detailMessage)
        {
            this.TraceData(new TraceEventCache(), Properties.Settings.Default.TraceSource_Name, TraceEventType.Error, 0, new TraceInformation
            {
                Message = message,
                DetailMessage = detailMessage,
                Category = Properties.Settings.Default.Category_None
            });
        }

        public override void Write(string message, string category)
        {
            lock (this._categorizedMessage)
            {
                char[] charsToAdd = ((message == null) ? "" : message).ToCharArray();
                string strCategory = (String.IsNullOrWhiteSpace(category)) ? Properties.Settings.Default.TraceSource_Name : category.Trim();
                if (this._categorizedInformation == null)
                    this._categorizedInformation = new TraceInformation { Category = strCategory };
                else if (this._categorizedInformation.Category != strCategory)
                {
                    // BUG: Causes an infinite wait for a lock on this._categorizedMessage
                    this.FlushCategorizedInformation();
                    this._categorizedInformation = new TraceInformation { Category = strCategory };
                }

                if (message != null)
                    this._currentLine = this._currentLine.Concat(message.ToCharArray()).ToArray();
                char[] newLineChars = Environment.NewLine.ToCharArray();
                while (this._currentLine.Length > 0)
                {
                    char[] nextLine = this._currentLine.TakeWhile(c => !newLineChars.Any(n => n == c)).ToArray();
                    if (nextLine.Length == this._currentLine.Length)
                        break;
                    this._currentLine = this._currentLine.Skip(nextLine.Length).ToArray();
                    this._categorizedMessage.Append((new String(this._currentIndent.Concat(nextLine).ToArray())).TrimEnd());
                    this._currentIndent = new char[0];
                    if (this._currentLine.Length == 0)
                        break;
                    this._categorizedMessage.Append(newLineChars);
                    this._currentLine = this._currentLine.Skip((newLineChars.SequenceEqual(this._currentLine.Take(newLineChars.Length))) ? newLineChars.Length : 1).ToArray();
                }
            }
        }

        private void FlushCategorizedInformation()
        {
            TraceInformation traceInfo;

            lock (this._categorizedMessage)
            {
                if (this._currentLine.Length == 0)
                {
                    if (this._categorizedInformation == null)
                        return;

                    this._currentIndent = new char[0];
                }
                else
                {
                    char[] newLineChars = Environment.NewLine.ToCharArray();
                    do
                    {
                        char[] nextLine = this._currentLine.TakeWhile(c => !newLineChars.Any(n => n == c)).ToArray();
                        this._currentLine = this._currentLine.Skip(nextLine.Length).ToArray();
                        this._categorizedMessage.Append(new String(this._currentIndent.Concat(nextLine).ToArray()).TrimEnd());
                        this._currentIndent = new char[0];
                        if (this._currentLine.Length == 0)
                            break;
                        this._categorizedMessage.Append(newLineChars);
                        this._currentLine = this._currentLine.Skip(1).ToArray();
                    } while (this._currentLine.Length > 0);
                }

                string text = this._categorizedMessage.ToString().Trim();
                this._categorizedMessage.Clear();
                if (text.Contains(Environment.NewLine))
                {
                    string[] parts = text.Split(new string[] { Environment.NewLine }, 2, StringSplitOptions.None);
                    this._categorizedInformation.Message = parts[0];
                    this._categorizedInformation.DetailMessage = parts[1];
                }
                else
                    this._categorizedInformation.Message = text;
                traceInfo = this._categorizedInformation;
                this._categorizedInformation = null;
                this._currentLine = new char[0];
                this._currentIndent = new char[0];
            }

            // BUG: TraceData calls this method, creating a potential endless loop
            this.TraceData(new TraceEventCache(), Properties.Settings.Default.TraceSource_Name, TraceEventType.Verbose, 0, traceInfo);
        }

        public override void Write(string message)
        {
            this.Write(message, Properties.Settings.Default.Category_None);
        }

        public override void Write(object o)
        {
            this.WriteLine(o, Properties.Settings.Default.Category_None);
        }

        public override void Write(object o, string category)
        {
            if (o == null)
                return;

            this.Write(o.ToString(), category);
        }

        protected override void WriteIndent()
        {
            this._currentIndent = this._currentIndent.Concat(new char[] { '\t' }).ToArray();
        }

        public override void WriteLine(string message)
        {
            this.WriteLine(message, Properties.Settings.Default.Category_None);
        }

        public override void WriteLine(string message, string category)
        {
            this.Write((message == null) ? Environment.NewLine : message + Environment.NewLine, category);
        }

        public override void WriteLine(object o)
        {
            this.WriteLine(o, Properties.Settings.Default.Category_None);
        }

        public override void WriteLine(object o, string category)
        {
            this.WriteLine((o == null) ? "" : o.ToString(), category);
        }

        public override void Flush()
        {
            this.FlushCategorizedInformation();
        }

        public override void Close()
        {
            this.Flush();
        }
    }
}
