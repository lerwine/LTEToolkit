using System;
using System.Windows.Data;

namespace Erwine.Leonard.T.Toolkit.WPF.Converters
{
    /// <summary>
    /// Base class for strongly-typed value converters
    /// </summary>
    /// <typeparam name="TSource">>The type of value expected to be produced by the binding source.</typeparam>
    /// <typeparam name="TTarget">Type type of target value intended to be used by the derrived converter.</typeparam>
    public abstract class ValueConverter<TSource, TTarget> : System.Windows.DependencyObject, System.Windows.Data.IValueConverter
    {
        public abstract TTarget NullValue { get; set; }

        /// <summary>
        /// Converts <typeparamref name="TSource"/> to <typeparamref name="TTarget"/>.
        /// </summary>
        /// <param name="value">The <typeparamref name="TSource"/> value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A <typeparamref name="TTarget"/> value. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            if (targetType != null && targetType.IsInstanceOfType(value))
                return value;

            object source = (value is TSource) ? value : this.OnOnConvertToSource(value);

            TTarget result = (source == null) ? this.NullValue : this.OnConvertToTarget((TSource)source);

            if (result != null && targetType != null && !targetType.IsInstanceOfType(result))
                return System.Convert.ChangeType(result, targetType);

            return result;
        }

        /// <summary>
        /// Converts a <typeparamref name="TSource"/> value back to a <typeparamref name="TTarget"/> value.
        /// </summary>
        /// <param name="value">The <typeparamref name="TTarget"/> value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Overridden in derrived classes to convert a data bound <typeparamref name="TSource"/> value to a <typeparamref name="TTarget"/> value.
        /// </summary>
        /// <param name="value"><typeparamref name="TSource"/> value to be converted.</param>
        /// <returns><typeparamref name="TTarget"/> value which was convereted from a <typeparamref name="TSource"/> value or null.</returns>
        protected abstract TTarget OnConvertToTarget(TSource value);

        /// <summary>
        /// Ensures that the value produced by the binding source is converted to a <typeparamref name="TSource"/> value.
        /// </summary>
        /// <param name="value">value to convert.</param>
        /// <returns>a <typeparamref name="TSource"/> value or null.</returns>
        protected virtual TSource OnOnConvertToSource(object value)
        {
            return (TSource)(System.Convert.ChangeType(value, typeof(TSource)));
        }
    }
}
