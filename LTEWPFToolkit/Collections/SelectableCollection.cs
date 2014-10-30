using Erwine.Leonard.T.Toolkit.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erwine.Leonard.T.Toolkit.WPF.Collections
{ 
    /// <summary>
    /// Represents a dynamic data collection that provides notifications when items get added, removed, selected, or when the whole list is refreshed.
    /// </summary>
    /// <typeparam name="T">The type of <seealso cref="Erwine.Leonard.T.Toolkit.WPF.Collections.ISelectable"/> elements in the collection.</typeparam>
    [Serializable]
    public class SelectableCollection<T> : ObservableCollection<T>, Erwine.Leonard.T.Toolkit.Collections.ISelectableCollection<T>
        where T : class, ISelectable
    {
        #region Construction and initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Erwine.Leonard.T.Toolkit.WPF.Collections.Erwine.Leonard.T.Toolkit.WPF.Collections&lt;T&gt;"/> class.
        /// </summary>
        public SelectableCollection()
            : base()
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Erwine.Leonard.T.Toolkit.WPF.Collections.Erwine.Leonard.T.Toolkit.WPF.Collections&lt;T&gt;"/>
        /// class that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="collection"/> parameter cannot be null.</exception>
        public SelectableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Erwine.Leonard.T.Toolkit.WPF.Collections.Erwine.Leonard.T.Toolkit.WPF.Collections&lt;T&gt;"/>
        /// class that contains elements copied from the specified list.
        /// </summary>
        /// <param name="list">The list from which the elements are copied.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="list"/> parameter cannot be null.</exception>
        public SelectableCollection(List<T> list)
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes collection. This gets called from the constructor.
        /// </summary>
        protected virtual void Initialize()
        {
            foreach (T item in this.GetDistinctInstances())
            {
                item.IsSelectedChanged += this.Item_IsSelectedChanged;
                item.Selected += this.Item_Selected;
                item.Deselected += this.Item_Deselected;
            }
        }

        #endregion

        #region Utility Methods

        public IEnumerable<T> GetDistinctInstances()
        {
            return this.GetDistinctInstances(null);
        }

        public IEnumerable<T> GetDistinctInstances(bool? isSelected)
        {
            ObjectReferenceEqualityComparer<T> comparer = new ObjectReferenceEqualityComparer<T>();
            IEnumerable<T> result = this.Where(i => i != null).Distinct(comparer);

            if (isSelected.HasValue)
                return result.Where(i => i.IsSelected = isSelected.Value);

            return result;
        }

        #endregion

        #region Item Events

        #region ItemSelectionChanged Event Members

        public event ValueEventHandler<T> ItemSelectionChanged;

        private void Item_IsSelectedChanged(object sender, EventArgs e)
        {
            this.RaiseItemSelectionChanged(sender as T);
        }

        private void RaiseItemSelectionChanged(T item)
        {
            this.OnItemSelectionChanged(new ValueEventArgs<T>(item));
        }

        protected virtual void OnItemSelectionChanged(ValueEventArgs<T> args)
        {
            if (this.ItemSelectionChanged != null)
                this.ItemSelectionChanged(this, args);
        }

        #endregion

        #region ItemSelected Event Members

        public event ValueEventHandler<T> ItemSelected;

        private void Item_Selected(object sender, EventArgs e)
        {
            this.RaiseItemSelected(sender as T);
        }

        private void RaiseItemSelected(T item)
        {
            this.OnItemSelected(new ValueEventArgs<T>(item));
        }

        protected virtual void OnItemSelected(ValueEventArgs<T> args)
        {
            if (this.ItemSelected != null)
                this.ItemSelected(this, args);
        }

        #endregion

        #region ItemDeselected Event Members

        public event ValueEventHandler<T> ItemDeselected;

        private void Item_Deselected(object sender, EventArgs e)
        {
            this.RaiseItemDeselected(sender as T);
        }

        private void RaiseItemDeselected(T item)
        {
            this.OnItemDeselected(new ValueEventArgs<T>(item));
        }

        protected virtual void OnItemDeselected(ValueEventArgs<T> args)
        {
            if (this.ItemDeselected != null)
                this.ItemDeselected(this, args);
        }

        #endregion

        #region ItemsCleared Event Members

        public event ValueEventHandler<T[]> ItemsCleared;

        private void RaiseItemsCleared(T[] items, int prevousCount)
        {
            foreach (T i in items)
            {
                i.IsSelectedChanged -= this.Item_IsSelectedChanged;
                i.Selected -= this.Item_Selected;
                i.Deselected -= this.Item_Deselected;
            }

            this.OnItemsCleared(new ValueEventArgs<T[]>(items));
        }

        protected virtual void OnItemsCleared(ValueEventArgs<T[]> args)
        {
            if (this.ItemsCleared != null)
                this.ItemsCleared(this, args);
        }

        #endregion

        #region NewInstanceInserted Event Members

        public event IndexedValueEventHandler<T> NewInstanceInserted;

        private void RaiseNewInstanceInserted(int index, T item)
        {
            item.IsSelectedChanged += this.Item_IsSelectedChanged;
            item.Selected += this.Item_Selected;
            item.Deselected += this.Item_Deselected;

            this.OnNewInstanceInserted(new IndexedValueEventArgs<T>(index, item));
        }

        protected virtual void OnNewInstanceInserted(IndexedValueEventArgs<T> args)
        {
            if (this.NewInstanceInserted != null)
                this.NewInstanceInserted(this, args);
        }

        #endregion

        #region DuplicateInstanceInserted Event Members

        public event IndexedValueEventHandler<T> DuplicateInstanceInserted;

        private void RaiseDuplicateInstanceInserted(int index, T item)
        {
            this.OnDuplicateInstanceInserted(new IndexedValueEventArgs<T>(index, item));
        }

        protected virtual void OnDuplicateInstanceInserted(IndexedValueEventArgs<T> args)
        {
            if (this.DuplicateInstanceInserted != null)
                this.DuplicateInstanceInserted(this, args);
        }

        #endregion

        #region DuplicateInstanceRemoved Event Members

        public event IndexedValueEventHandler<T> DuplicateInstanceRemoved;

        private void RaiseDuplicateInstanceRemoved(int index, T item)
        {
            this.OnDuplicateInstanceRemoved(new IndexedValueEventArgs<T>(index, item));
        }

        protected virtual void OnDuplicateInstanceRemoved(IndexedValueEventArgs<T> args)
        {
            if (this.DuplicateInstanceRemoved != null)
                this.DuplicateInstanceRemoved(this, args);
        }

        #endregion

        #region FinalInstanceRemoved Event Members

        public event IndexedValueEventHandler<T> FinalInstanceRemoved;

        private void RaiseFinalInstanceRemoved(int index, T item)
        {
            item.IsSelectedChanged -= this.Item_IsSelectedChanged;
            item.Selected -= this.Item_Selected;
            item.Deselected -= this.Item_Deselected;

            this.OnFinalInstanceRemoved(new IndexedValueEventArgs<T>(index, item));
        }

        protected virtual void OnFinalInstanceRemoved(IndexedValueEventArgs<T> args)
        {
            if (this.FinalInstanceRemoved != null)
                this.FinalInstanceRemoved(this, args);
        }

        #endregion

        #region ItemMoved Event Members

        public event IndexedMoveValueEventHandler<T> ItemMoved;

        private void RaiseItemMoved(int oldIndex, int newIndex, T item)
        {
            this.OnItemMoved(new IndexedMoveValueEventArgs<T>(oldIndex, newIndex, item));
        }

        protected virtual void OnItemMoved(IndexedMoveValueEventArgs<T> args)
        {
            if (this.ItemMoved != null)
                this.ItemMoved(this, args);
        }

        #endregion

        #region IndexValuesShifted Event Members

        public event IndexShiftEventHandler IndexValuesShifted;

        private void RaiseIndexValuesShifted(params IndexShiftParameter[] parameters)
        {
            this.OnIndexValuesShifted(new IndexShiftEventArgs(parameters));
        }

        protected virtual void OnIndexValuesShifted(IndexShiftEventArgs args)
        {
            if (this.IndexValuesShifted != null)
                this.IndexValuesShifted(this, args);
        }

        #endregion

        #endregion

        #region Overrides

        protected override void ClearItems()
        {
            int count = this.Count;
            T[] items = this.GetDistinctInstances().ToArray();

            base.ClearItems();

            this.RaiseItemsCleared(items, count);
        }

        protected override void InsertItem(int index, T item)
        {
            bool isNewReference = item != null && !this.Any(i => i != null && Object.ReferenceEquals(i, item));

            base.InsertItem(index, item);

            if (isNewReference)
                this.RaiseNewInstanceInserted(index, item);
            else
                this.RaiseDuplicateInstanceInserted(index, item);

            this.RaiseIndexValuesShifted(new IndexShiftParameter(index + 1, 1, this.Count - (index + 1)));
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);
            
            if (oldIndex == newIndex)
                return;

            if (oldIndex < newIndex)
                this.RaiseIndexValuesShifted(new IndexShiftParameter(oldIndex, -1, newIndex - oldIndex), new IndexShiftParameter(newIndex, newIndex - oldIndex, 1));
            else
                this.RaiseIndexValuesShifted(new IndexShiftParameter(oldIndex, newIndex - oldIndex, 1), new IndexShiftParameter(newIndex, 1, oldIndex - newIndex));
        }

        protected override void RemoveItem(int index)
        {
            T item = (index < 0 || index >= this.Count) ? null : this[index];

            base.RemoveItem(index);

            if (item == null || this.Any(i => i != null && Object.ReferenceEquals(i, item)))
                this.RaiseDuplicateInstanceRemoved(index, item);
            else
                this.RaiseFinalInstanceRemoved(index, item);

            this.RaiseIndexValuesShifted(new IndexShiftParameter(index, -1, this.Count - index));
        }

        protected override void SetItem(int index, T item)
        {
            T oldItem = (index < 0 || index >= this.Count) ? null : this[index];

            bool isNewReference = item != null && !this.Any(i => i != null && Object.ReferenceEquals(i, item));

            base.SetItem(index, item);

            if ((item == null) ? (oldItem == null) : (oldItem != null && Object.ReferenceEquals(item, oldItem)))
                return;

            if (item == null || this.Any(i => i != null && Object.ReferenceEquals(i, item)))
                this.RaiseDuplicateInstanceRemoved(index, item);
            else
                this.RaiseFinalInstanceRemoved(index, item);

            if (isNewReference)
                this.RaiseNewInstanceInserted(index, item);
            else
                this.RaiseDuplicateInstanceInserted(index, item);
        }

        #endregion
    }
}
