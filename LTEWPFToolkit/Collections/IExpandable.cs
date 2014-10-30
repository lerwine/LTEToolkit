using System.Windows.Input;

namespace Erwine.Leonard.T.Toolkit.WPF.Collections
{
    public interface IExpandable : Erwine.Leonard.T.Toolkit.Collections.IExpandable
    {
        ICommand ToggleExpandCommand { get; }
        ICommand ExpandCommand { get; }
        ICommand CollapseCommand { get; }
    }

    public interface IExpandable<T> : Erwine.Leonard.T.Toolkit.Collections.IExpandable<T>, IExpandable
    {
    }
}
