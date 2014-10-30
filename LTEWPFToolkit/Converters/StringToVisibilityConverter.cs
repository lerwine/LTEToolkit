using System.Windows;

namespace Erwine.Leonard.T.Toolkit.WPF.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.String"/> source values to <see cref="System.Windows.Visibility"/> target values.
    /// </summary>
    public class StringToVisibilityConverter : StringToStructValueConverter<Visibility>
    {
        public static readonly DependencyProperty NonEmptyValueProperty =
            DependencyProperty.Register("NonEmptyValue", typeof(Visibility?), typeof(StringToVisibilityConverter), new PropertyMetadata(Visibility.Visible));

        public override Visibility? EmptyValue
        {
            get { return (Visibility?)(this.GetValue(StringToVisibilityConverter.EmptyValueProperty)); }
            set { this.SetValue(StringToVisibilityConverter.EmptyValueProperty, value); }
        }

        public static readonly DependencyProperty EmptyValueProperty =
               DependencyProperty.Register("EmptyValue", typeof(Visibility?), typeof(StringToVisibilityConverter), new PropertyMetadata(Visibility.Collapsed));

        public override Visibility? NonEmptyValue
        {
            get { return (Visibility?)(this.GetValue(StringToVisibilityConverter.NonEmptyValueProperty)); }
            set { this.SetValue(StringToVisibilityConverter.NonEmptyValueProperty, value); }
        }
    }
}
