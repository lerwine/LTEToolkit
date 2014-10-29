using Erwine.Leonard.T.Toolkit.Events;
using System.Collections.Generic;

namespace Erwine.Leonard.T.Toolkit.Collections
{
    public interface ISelectableCollection<T> where T : class, ISelectable
    {
        event ValueEventHandler<T> ItemSelectionChanged;
        event ValueEventHandler<T> ItemSelected;
        event ValueEventHandler<T> ItemDeselected;
        event ValueEventHandler<T[]> ItemsCleared;
        event IndexedValueEventHandler<T> NewInstanceInserted;
        event IndexedValueEventHandler<T> DuplicateInstanceInserted;
        event IndexedValueEventHandler<T> FinalInstanceRemoved;
        event IndexedValueEventHandler<T> DuplicateInstanceRemoved;
        event IndexedMoveValueEventHandler<T> ItemMoved;
        event IndexShiftEventHandler IndexValuesShifted;
        IEnumerable<T> GetDistinctInstances();
        IEnumerable<T> GetDistinctInstances(bool? isSelected);
    }
}
