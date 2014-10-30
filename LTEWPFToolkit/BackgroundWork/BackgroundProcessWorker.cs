using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Erwine.Leonard.T.Toolkit.WPF.BackgroundWork
{
    public class BackgroundProcessWorker : IBackgroundProcessWorker
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public CancellationTokenSource CancellationTokenSource
        {
            get { return this._cancellationTokenSource; }
        }

        public IBackgroundProcessViewModel ViewModel { get; set; }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Cleanup(Task task)
        {
            throw new NotImplementedException();
        }
    }
}
