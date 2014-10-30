using System.Windows.Input;

namespace Erwine.Leonard.T.Toolkit.WPF.Collections
{
    public interface ISelectable : Erwine.Leonard.T.Toolkit.Collections.ISelectable
    {
        ICommand ToggleSelectCommand { get; }
        ICommand SelectCommand { get; }
        ICommand DeselectCommand { get; }
    }

    public interface ISelectable<T> : Erwine.Leonard.T.Toolkit.Collections.ISelectable<T>, ISelectable
    {
    }
}
