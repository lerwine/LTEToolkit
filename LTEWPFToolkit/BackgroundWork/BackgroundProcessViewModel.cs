using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Erwine.Leonard.T.Toolkit.WPF.BackgroundWork
{
    public class BackgroundProcessViewModel : DependencyObject, IBackgroundProcessViewModel
    {
        private object _syncRoot = new object();
        private object _currentTask = null;

        #region HostWindow Property Members

        public const string PropertyName_HostWindow = "HostWindow";

        public static readonly DependencyProperty HostWindowProperty =
            DependencyProperty.Register(BackgroundProcessViewModel.PropertyName_HostWindow, typeof(IBackgroundProcessWindow), typeof(BackgroundProcessViewModel),
                new PropertyMetadata(null, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as BackgroundProcessViewModel).OnHostWindowPropertyChanged((IBackgroundProcessWindow)(e.OldValue), (IBackgroundProcessWindow)(e.NewValue))));

        public IBackgroundProcessWindow HostWindow
        {
            get { return (IBackgroundProcessWindow)(this.GetValue(BackgroundProcessViewModel.HostWindowProperty)); }
            set { this.SetValue(BackgroundProcessViewModel.HostWindowProperty, value); }
        }

        protected virtual void OnHostWindowPropertyChanged(IBackgroundProcessWindow oldValue, IBackgroundProcessWindow newValue)
        {
            if (oldValue != null)
            {
                oldValue.Activated -= this.HostWindow_Activated;
                oldValue.Closing -= this.HostWindow_Closing;
                oldValue.Closed -= this.HostWindow_Closed;
            }

            if (newValue != null)
            {
                newValue.Activated += this.HostWindow_Activated;
                newValue.Closing += this.HostWindow_Closing;
                newValue.Closed += this.HostWindow_Closed;
            }
        }

        private void HostWindow_Activated(object sender, EventArgs e)
        {
            lock (this._syncRoot)
            {
                if (this._currentTask == null && this.HostWindow != null && this.HostWindow.Worker != null)
                {
                    this.HostWindow.Worker.ViewModel = this;
                    Task task;
                    if (this.HostWindow.Worker.CancellationTokenSource == null)
                        task = Task.Factory.StartNew(this.HostWindow.Worker.Start);
                    else
                        task = Task.Factory.StartNew(this.HostWindow.Worker.Start, this.HostWindow.Worker.CancellationTokenSource.Token);
                    task.ContinueWith(this.HostWindow.Worker.Cleanup)
                        .ContinueWith(this.Worker_Finished);
                    this._currentTask = task;
                }
            }
        }

        private void Worker_Finished(Task task)
        {
            Task currentTask;

            lock (this._syncRoot)
            {
                currentTask = this._currentTask as Task;
                this._currentTask = new object();
            }

            if (currentTask == null)
                return;
            
            currentTask.Dispose();

            this.HostWindow.CloseWindow_Safe(this.DialogResult);
        }

        private void HostWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lock (this._syncRoot)
            {
                if (this._currentTask != null && this._currentTask is Task)
                {
                    this.Cancel();
                    e.Cancel = true;
                }
            }
        }

        private void HostWindow_Closed(object sender, EventArgs e)
        {
            lock (this._syncRoot)
                this._currentTask = null;
        }

        #endregion

        #region Message Property Members

        public const string PropertyName_Message = "Message";

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(BackgroundProcessViewModel.PropertyName_Message, typeof(string), typeof(BackgroundProcessViewModel),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as BackgroundProcessViewModel).OnMessagePropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string Message
        {
            get { return this.GetValue(BackgroundProcessViewModel.MessageProperty) as string; }
            set { this.SetValue(BackgroundProcessViewModel.MessageProperty, value); }
        }

        protected virtual void OnMessagePropertyChanged(string oldValue, string newValue)
        {
            if (newValue == null)
            {
                this.Message = "";
                return;
            }

            string s;
            if ((s = newValue.Trim()) != s)
                this.Message = s;
        }

        public void SetMessage_Safe(string text)
        {
            if (this.Dispatcher.CheckAccess())
                this.Message = text;
            else
                this.Dispatcher.Invoke(new Action<string>((string s) => this.Message = s), text);
        }

        public void SetMessage_Safe(string format, params object[] args)
        {
            if (this.Dispatcher.CheckAccess())
                this.Message = String.Format(format, args);
            else
                this.Dispatcher.Invoke(new Action<string, object[]>((string f, object[] a) => this.Message = String.Format(f, a)), format, args);
        }

        #endregion

        #region Details Property Members

        public const string PropertyName_Details = "Details";

        public static readonly DependencyProperty DetailsProperty =
            DependencyProperty.Register(BackgroundProcessViewModel.PropertyName_Details, typeof(string), typeof(BackgroundProcessViewModel),
                new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as BackgroundProcessViewModel).OnDetailsPropertyChanged(e.OldValue as string, e.NewValue as string)));

        public string Details
        {
            get { return this.GetValue(BackgroundProcessViewModel.DetailsProperty) as string; }
            set { this.SetValue(BackgroundProcessViewModel.DetailsProperty, value); }
        }

        protected virtual void OnDetailsPropertyChanged(string oldValue, string newValue)
        {
            if (newValue == null)
                this.Details = "";
        }

        public void SetDetails_Safe(string text)
        {
            if (this.Dispatcher.CheckAccess())
                this.Details = text;
            else
                this.Dispatcher.Invoke(new Action<string>((string s) => this.Details = s), text);
        }

        public void SetDetails_Safe(string format, params object[] args)
        {
            if (this.Dispatcher.CheckAccess())
                this.Details = String.Format(format, args);
            else
                this.Dispatcher.Invoke(new Action<string, object[]>((string f, object[] a) => this.Details = String.Format(f, a)), format, args);
        }

        #endregion

        #region DialogResult Property Members

        public const string PropertyName_DialogResult = "DialogResult";

        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.Register(BackgroundProcessViewModel.PropertyName_DialogResult, typeof(bool), typeof(BackgroundProcessViewModel),
                new PropertyMetadata(true));

        public bool DialogResult
        {
            get { return (bool)(this.GetValue(BackgroundProcessViewModel.DialogResultProperty)); }
            set { this.SetValue(BackgroundProcessViewModel.DialogResultProperty, value); }
        }

        public void SetDialogResult_Safe(bool value)
        {
            if (this.Dispatcher.CheckAccess())
                this.DialogResult = value;
            else
                this.Dispatcher.Invoke(new Action<bool>((bool r) => this.DialogResult = r), value);
        }

        #endregion

        #region IsPausedDisabled Property Members

        public const string PropertyName_IsPausedDisabled = "IsPausedDisabled";

        public static readonly DependencyProperty IsPausedDisabledProperty =
            DependencyProperty.Register(BackgroundProcessViewModel.PropertyName_IsPausedDisabled, typeof(bool), typeof(BackgroundProcessViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as BackgroundProcessViewModel).OnIsPausedDisabledPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool IsPausedDisabled
        {
            get { return (bool)(this.GetValue(BackgroundProcessViewModel.IsPausedDisabledProperty)); }
            set { this.SetValue(BackgroundProcessViewModel.IsPausedDisabledProperty, value); }
        }

        protected virtual void OnIsPausedDisabledPropertyChanged(bool oldValue, bool newValue)
        {
            this.PauseCommand.IsEnabled = !newValue;
        }

        #endregion

        #region Pause Command Property Members

        private Events.RelayCommand _pauseCommand = null;

        public Events.RelayCommand PauseCommand
        {
            get
            {
                if (this._pauseCommand == null)
                {
                    this._pauseCommand = new Events.RelayCommand(this.OnPause);
                    this._pauseCommand.IsEnabled = !this.IsPausedDisabled;
                }

                return this._pauseCommand;
            }
        }

        protected virtual void OnPause(object parameter)
        {
            // TODO: Implement OnPause Logic
        }

        #endregion

        #region IsCancelDisabled Property Members

        public const string PropertyName_IsCancelDisabled = "IsCancelDisabled";

        public static readonly DependencyProperty IsCancelDisabledProperty =
            DependencyProperty.Register(BackgroundProcessViewModel.PropertyName_IsCancelDisabled, typeof(bool), typeof(BackgroundProcessViewModel),
                new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    (d as BackgroundProcessViewModel).OnIsCancelDisabledPropertyChanged((bool)(e.OldValue), (bool)(e.NewValue))));

        public bool IsCancelDisabled
        {
            get { return (bool)(this.GetValue(BackgroundProcessViewModel.IsCancelDisabledProperty)); }
            set { this.SetValue(BackgroundProcessViewModel.IsCancelDisabledProperty, value); }
        }

        protected virtual void OnIsCancelDisabledPropertyChanged(bool oldValue, bool newValue)
        {
            this.CancelCommand.IsEnabled = !newValue;
        }

        #endregion

        #region Cancel Command Property Members

        private Events.RelayCommand _cancelCommand = null;

        public Events.RelayCommand CancelCommand
        {
            get
            {
                if (this._cancelCommand == null)
                {
                    this._cancelCommand = new Events.RelayCommand(this.OnCancel);
                    this._cancelCommand.IsEnabled = !this.IsCancelDisabled;
                }

                return this._cancelCommand;
            }
        }

        protected virtual void OnCancel(object parameter)
        {
            this.Cancel();
        }

        public void Cancel()
        {
            IBackgroundProcessWindow window = this.HostWindow;
            if (window == null)
                return;

            IBackgroundProcessWorker worker = window.Worker;
            if (worker != null && worker.CancellationTokenSource != null && !worker.CancellationTokenSource.IsCancellationRequested)
                worker.CancellationTokenSource.Cancel();
        }

        #endregion

    }
}
