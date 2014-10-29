using System;
using System.Linq;

namespace Erwine.Leonard.T.Toolkit.Tracing
{
    [Serializable]
    public class TraceInformation : IEquatable<TraceInformation>
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public long Timestamp { get; set; }
        public string Message { get; set; }
        public Guid? ActivityId { get; set; }
        public string DetailMessage { get; set; }
        public string Category { get; set; }
        public string Callstack { get; set; }
        public string[] LogicalOperationStack { get; set; }
        public int ProcessId { get; set; }
        public string ThreadId { get; set; }
        public string Source { get; set; }
        public System.Diagnostics.TraceEventType EventType { get; set; }

        public TraceInformation()
        {
            this.Id = Guid.NewGuid();
        }

        internal static TraceInformation Create(params object[] data)
        {
            object[] d = (data == null) ? new object[0] : data.SkipWhile(o => o == null).ToArray();
            if (d.Length == 0)
                return new TraceInformation();

            TraceInformation result;
            if (d[0] is TraceInformation)
            {
                result = d[0] as TraceInformation;
                d = d.Skip(1).ToArray();
            }
            else
                result = new TraceInformation();

            string[] msgs = d.Select(o => (o == null) ? "" : o.ToString()).ToArray();

            if (String.IsNullOrWhiteSpace(result.Message))
            {
                result.Message = (msgs.Length > 0) ? msgs[0].Trim() : "";
                msgs = msgs.Skip(1).ToArray();
            }

            result.DetailMessage = (msgs.Length == 0) ? "" : String.Join(Environment.NewLine, msgs).Trim();

            return result;
        }

        public bool Equals(TraceInformation other)
        {
            return (other != null && Object.ReferenceEquals(this, other));
        }

        public override bool Equals(object obj)
        {
            return (obj != null && Object.ReferenceEquals(this, obj));
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
