using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Erwine.Leonard.T.Toolkit.WPF.BackgroundWork
{
    public interface IBackgroundProcessWorker
    {
        CancellationTokenSource CancellationTokenSource { get; }
        IBackgroundProcessViewModel ViewModel { get; set; }
        void Start();
        void Cleanup(Task task);
    }
}
