using System.Windows;

namespace Erwine.Leonard.T.Toolkit.WPF.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.Array"/> and <see cref="System.Collections.ICollection"/> source values to <see cref="System.Windows.Visibility"/> target values.
    /// </summary>
    public class CollectionToVisibilityConverter : CollectionToStructValueConverter<Visibility>
    {
        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.CollectionToVisibilityConverter.NonEmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty NonEmptyValueProperty =
            DependencyProperty.Register("NonEmptyValue", typeof(Visibility?), typeof(CollectionToVisibilityConverter), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.CollectionToVisibilityConverter.EmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register("EmptyValue", typeof(Visibility?), typeof(CollectionToVisibilityConverter), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// <see cref="Nullable&lt;System.Windows.Visibility&gt;"/> value to use for non-empty, non-null collections
        /// </summary>
        public override Visibility? NonEmptyValue
        {
            get { return (Visibility?)(this.GetValue(CollectionToVisibilityConverter.NonEmptyValueProperty)); }
            set { this.SetValue(CollectionToVisibilityConverter.NonEmptyValueProperty, value); }
        }

        /// <summary>
        /// <see cref="Nullable&lt;System.Windows.Visibility&gt;"/> value to use for empty, non-null collections
        /// </summary>
        public override Visibility? EmptyValue
        {
            get { return (Visibility?)(this.GetValue(CollectionToVisibilityConverter.EmptyValueProperty)); }
            set { this.SetValue(CollectionToVisibilityConverter.EmptyValueProperty, value); }
        }
    }
}
