using System.Windows;

namespace Erwine.Leonard.T.Toolkit.WPF.Converters
{
    /// <summary>
    /// Base class for converting from one <see cref="System.ValueType"/> to another <see cref="System.ValueType"/>.
    /// </summary>
    /// <typeparam name="TSource">>The type of value expected to be produced by the binding source.</typeparam>
    /// <typeparam name="TTarget">Type type of target value intended to be used by the derrived converter.</typeparam>
    public abstract class StructToStructValueConverter<TSource, TTarget> : ValueConverter<TSource?, TTarget?>
        where TSource : struct
        where TTarget : struct
    {
        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.StructToStructValueConverter&lt;TSource, TTarget&gt;.NullValue"/> property.
        /// </summary>
        public static readonly DependencyProperty NullValueProperty =
               DependencyProperty.Register("NullValue", typeof(TTarget?), typeof(StructToStructValueConverter<TSource, TTarget>), new PropertyMetadata(null));

        /// <summary>
        /// The <see cref="System.Nullable&lt;System.Boolean&gt;"/> value to use for null source values.
        /// </summary>
        public override TTarget? NullValue
        {
            get { return (TTarget?)(this.GetValue(StructToStructValueConverter<TSource, TTarget>.NullValueProperty)); }
            set { this.SetValue(StructToStructValueConverter<TSource, TTarget>.NullValueProperty, value); }
        }

        /// <summary>
        /// Ensures that the value produced by the binding source is converted to a <typeparamref name="TSource"/> value.
        /// </summary>
        /// <param name="value">value to convert.</param>
        /// <returns>a <typeparamref name="TSource"/> value or null.</returns>
        protected abstract TTarget? OnConvertToTarget(TSource value);

        /// <summary>
        /// Ensures that the value produced by the binding source is converted to a <typeparamref name="TSource"/> value.
        /// </summary>
        /// <param name="value">value to convert.</param>
        /// <returns>a <typeparamref name="TSource"/> value or null.</returns>
        protected override TTarget? OnConvertToTarget(TSource? value)
        {
            if (value.HasValue)
                return this.NullValue;

            return this.OnConvertToTarget(value.Value);
        }
    }
}
