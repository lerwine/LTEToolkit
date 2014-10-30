namespace Erwine.Leonard.T.Toolkit.WPF.BackgroundWork
{
    public interface IBackgroundProcessViewModel
    {
        void SetDetails_Safe(string format, params object[] args);
        void SetDetails_Safe(string text);
        void SetDialogResult_Safe(bool value);
        void SetMessage_Safe(string format, params object[] args);
        void SetMessage_Safe(string text);
    }
}
