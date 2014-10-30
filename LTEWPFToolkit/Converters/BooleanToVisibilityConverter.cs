using System.Windows;

namespace Erwine.Leonard.T.Toolkit.WPF.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.Boolean"/> source values to <see cref="System.Windows.Visibility"/> target values.
    /// </summary>
    public class BooleanToVisibilityConverter : StructToStructValueConverter<bool, Visibility>
    {
        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.BooleanToVisibilityConverter.TrueValue"/> property.
        /// </summary>
        public static readonly DependencyProperty TrueValueProperty =
            DependencyProperty.Register("TrueValue", typeof(Visibility?), typeof(BooleanToVisibilityConverter), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.BooleanToVisibilityConverter.FalseValue"/> property.
        /// </summary>
        public static readonly DependencyProperty FalseValueProperty =
            DependencyProperty.Register("FalseValue", typeof(Visibility?), typeof(BooleanToVisibilityConverter), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Windows.Visibility&gt;"/> value to use for true <see cref="System.Boolean"/> source values.
        /// </summary>
        public Visibility? TrueValue
        {
            get { return (Visibility?)(this.GetValue(BooleanToVisibilityConverter.TrueValueProperty)); }
            set { this.SetValue(BooleanToVisibilityConverter.TrueValueProperty, value); }
        }

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Windows.Visibility&gt;"/> value to use for false <see cref="System.Boolean"/> source values.
        /// </summary>
        public Visibility? FalseValue
        {
            get { return (Visibility?)(this.GetValue(BooleanToVisibilityConverter.FalseValueProperty)); }
            set { this.SetValue(BooleanToVisibilityConverter.FalseValueProperty, value); }
        }

        protected override Visibility? OnConvertToTarget(bool value)
        {
            return (value) ? this.TrueValue : this.FalseValue;
        }
    }
}
