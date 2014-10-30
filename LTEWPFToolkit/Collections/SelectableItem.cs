using Erwine.Leonard.T.Toolkit.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Erwine.Leonard.T.Toolkit.WPF.Collections
{
    public class SelectableItem : DependencyObject, ISelectable<int>
    {
        public int Value { get; set; }

        public SelectableItem() : base() { }

        public SelectableItem(int value)
            : base()
        {

        }

        #region IsSelected Property Members

        public event ValueEventHandler<bool> IsSelectedChanged;

        public const string PropertyName_IsSelected = "IsSelected";

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(SelectableItem.PropertyName_IsSelected, typeof(bool), typeof(SelectableItem),
                new PropertyMetadata(false,
                    (DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as SelectableItem).RaiseIsSelectedPropertyChanged((bool)(e.NewValue))));

        public bool IsSelected
        {
            get { return (bool)(this.GetValue(SelectableItem.IsSelectedProperty)); }
            set { this.SetValue(SelectableItem.IsSelectedProperty, value); }
        }

        protected void RaiseIsSelectedPropertyChanged(bool value)
        {
            this.OnIsSelectedPropertyChanged(new ValueEventArgs<bool>(value));
        }

        protected virtual void OnIsSelectedPropertyChanged(ValueEventArgs<bool> args)
        {

            if (this.IsSelectedChanged != null)
                this.IsSelectedChanged(this, args);
        }

        #endregion

        #region ToggleSelect Command Property Members

        private Events.RelayCommand _toggleSelectCommand = null;

        public ICommand ToggleSelectCommand
        {
            get
            {
                if (this._toggleSelectCommand == null)
                    this._toggleSelectCommand = new Events.RelayCommand(this.OnToggleSelect);

                return this._toggleSelectCommand;
            }
        }

        protected virtual void OnToggleSelect(object parameter)
        {
            this.ToggleSelect();
        }

        public void ToggleSelect()
        {
            this.IsSelected = !this.IsSelected;
        }

        #endregion

        #region Select Command Property Members

        public event EventHandler Selected;

        private Events.RelayCommand _selectCommand = null;

        public ICommand SelectCommand
        {
            get
            {
                if (this._selectCommand == null)
                    this._selectCommand = new Events.RelayCommand(this.OnSelect);

                return this._selectCommand;
            }
        }

        protected virtual void OnSelect(object parameter)
        {
            this.Select();
        }

        public void Select()
        {
            this.IsSelected = true;
        }

        #endregion

        #region Deselect Command Property Members

        public event EventHandler Deselected;

        private Events.RelayCommand _deselectCommand = null;

        public ICommand DeselectCommand
        {
            get
            {
                if (this._deselectCommand == null)
                    this._deselectCommand = new Events.RelayCommand(this.OnDeselect);

                return this._deselectCommand;
            }
        }

        protected virtual void OnDeselect(object parameter)
        {
            this.Deselect();
        }

        public void Deselect()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISelectable Members

        event EventHandler Toolkit.Collections.ISelectable.IsSelectedChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        #endregion
    }
}
