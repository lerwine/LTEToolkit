using System.Windows;

namespace Erwine.Leonard.T.Toolkit.WPF.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.Windows.Visibility"/> source values to <see cref="System.Boolean"/> target values.
    /// </summary>
    public class VisibilityToBooleanConverter : StructToStructValueConverter<Visibility, bool>
    {
        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.VisibilityToBooleanConverter.VisibleValue"/> property.
        /// </summary>
        public static readonly DependencyProperty VisibleValueProperty =
            DependencyProperty.Register("VisibleValue", typeof(bool?), typeof(VisibilityToBooleanConverter), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.VisibilityToBooleanConverter.CollapsedValue"/> property.
        /// </summary>
        public static readonly DependencyProperty CollapsedValueProperty =
            DependencyProperty.Register("CollapsedValue", typeof(bool?), typeof(VisibilityToBooleanConverter), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.VisibilityToBooleanConverter.HiddenValue"/> property.
        /// </summary>
        public static readonly DependencyProperty HiddenValueProperty =
            DependencyProperty.Register("HiddenValue", typeof(bool?), typeof(VisibilityToBooleanConverter), new PropertyMetadata(false));

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Boolean&gt;"/> value to use for <see cref="System.Windows.Visibility.Visible"/> source values.
        /// </summary>
        public bool? VisibleValue
        {
            get { return (bool?)(this.GetValue(VisibilityToBooleanConverter.VisibleValueProperty)); }
            set { this.SetValue(VisibilityToBooleanConverter.VisibleValueProperty, value); }
        }

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Boolean&gt;"/> value to use for <see cref="System.Windows.Visibility.Collapsed"/> source values.
        /// </summary>
        public bool? CollapsedValue
        {
            get { return (bool?)(this.GetValue(VisibilityToBooleanConverter.CollapsedValueProperty)); }
            set { this.SetValue(VisibilityToBooleanConverter.CollapsedValueProperty, value); }
        }

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Boolean&gt;"/> value to use for <see cref="System.Windows.Visibility.Hidden"/> source values.
        /// </summary>
        public bool? HiddenValue
        {
            get { return (bool?)(this.GetValue(VisibilityToBooleanConverter.HiddenValueProperty)); }
            set { this.SetValue(VisibilityToBooleanConverter.HiddenValueProperty, value); }
        }

        /// <summary>
        /// Ensures that the value produced by the binding source is converted to a <see name="System.Windows.Visibility"/> value.
        /// </summary>
        /// <param name="value">value to convert.</param>
        /// <returns>a <see name="System.Boolean"/> value or null.</returns>
        protected override bool? OnConvertToTarget(Visibility value)
        {
            switch (value)
            {
                case Visibility.Visible:
                    return this.VisibleValue;
                case Visibility.Hidden:
                    return this.HiddenValue;
            }

            return this.CollapsedValue;
        }
    }
}
