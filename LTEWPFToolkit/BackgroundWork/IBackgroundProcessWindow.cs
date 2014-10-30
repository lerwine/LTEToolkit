using System;
using System.ComponentModel;

namespace Erwine.Leonard.T.Toolkit.WPF.BackgroundWork
{
    public interface IBackgroundProcessWindow
    {
        void CloseWindow_Safe(bool dialogResult);
        bool GetIsActive_Safe();
        bool GetIsFocused_Safe();
        System.Windows.WindowState GetWindowState_Safe();
        IBackgroundProcessWorker Worker { get; set; }
        event EventHandler Activated;
        event EventHandler Deactivated;
        event EventHandler StateChanged;
        event CancelEventHandler Closing;
        event EventHandler Closed;
    }
}
