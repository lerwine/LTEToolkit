using System.Windows;

namespace Erwine.Leonard.T.Toolkit.WPF.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.Array"/> and <see cref="System.Collections.ICollection"/> source values to <see cref="System.Boolean"/> target values.
    /// </summary>
    public class CollectionToBooleanConverter : CollectionToStructValueConverter<bool>
    {
        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.CollectionToBooleanConverter.NonEmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty NonEmptyValueProperty =
            DependencyProperty.Register("NonEmptyValue", typeof(bool?), typeof(CollectionToBooleanConverter), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.CollectionToBooleanConverter.EmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register("EmptyValue", typeof(bool?), typeof(CollectionToBooleanConverter), new PropertyMetadata(false));

        /// <summary>
        /// <see cref="Nullable&lt;System.Boolean&gt;"/> value to use for non-empty, non-null collections
        /// </summary>
        public override bool? NonEmptyValue
        {
            get { return (bool?)(this.GetValue(CollectionToBooleanConverter.NonEmptyValueProperty)); }
            set { this.SetValue(CollectionToBooleanConverter.NonEmptyValueProperty, value); }
        }

        /// <summary>
        /// <see cref="Nullable&lt;System.Boolean&gt;"/> value to use for empty, non-null collections
        /// </summary>
        public override bool? EmptyValue
        {
            get { return (bool?)(this.GetValue(CollectionToBooleanConverter.EmptyValueProperty)); }
            set { this.SetValue(CollectionToBooleanConverter.EmptyValueProperty, value); }
        }
    }
}
