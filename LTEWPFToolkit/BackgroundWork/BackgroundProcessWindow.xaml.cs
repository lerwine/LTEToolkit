using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Erwine.Leonard.T.Toolkit.WPF.BackgroundWork
{
    /// <summary>
    /// Interaction logic for BackgroundProcessWindow.xaml
    /// </summary>
    public partial class BackgroundProcessWindow : Window, IBackgroundProcessWindow
    {
        #region Worker Property Members

        public const string PropertyName_Worker = "Worker";

        public static readonly DependencyProperty WorkerProperty =
            DependencyProperty.Register(BackgroundProcessWindow.PropertyName_Worker, typeof(IBackgroundProcessWorker), typeof(BackgroundProcessWindow),
                new PropertyMetadata(null));

        public IBackgroundProcessWorker Worker
        {
            get { return (IBackgroundProcessWorker)(this.GetValue(BackgroundProcessWindow.WorkerProperty)); }
            set { this.SetValue(BackgroundProcessWindow.WorkerProperty, value); }
        }

        #endregion

        public BackgroundProcessWindow()
        {
            InitializeComponent();

            BackgroundProcessViewModel viewModel = this.FindResource("ViewModel") as BackgroundProcessViewModel;
            viewModel.HostWindow = this;
        }

        public bool GetIsFocused_Safe()
        {
            if (this.Dispatcher.CheckAccess())
                return this.IsFocused;

            return (bool)(this.Dispatcher.Invoke(new Func<bool>(() => this.IsFocused)));
        }

        public bool GetIsActive_Safe()
        {
            if (this.Dispatcher.CheckAccess())
                return this.IsActive;

            return (bool)(this.Dispatcher.Invoke(new Func<bool>(() => this.IsActive)));
        }

        public WindowState GetWindowState_Safe()
        {
            if (this.Dispatcher.CheckAccess())
                return this.WindowState;

            return (WindowState)(this.Dispatcher.Invoke(new Func<WindowState>(() => this.WindowState)));
        }

        public void CloseWindow_Safe(bool dialogResult)
        {
            Action<bool> action = (bool r) =>
            {
                this.DialogResult = r;
                this.Close();
            };

            if (this.Dispatcher.CheckAccess())
                action(dialogResult);
            else
                this.Dispatcher.Invoke(action, dialogResult);
        }
    }
}
