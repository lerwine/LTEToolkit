﻿using System.Windows;

namespace Erwine.Leonard.T.Toolkit.WPF.Converters
{
    /// <summary>
    /// Logic for binding <see cref="System.String"/> source values to <see cref="System.Boolean"/> target values.
    /// </summary>
    public class StringToBooleanConverter : StringToStructValueConverter<bool>
    {
        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.StringToBoolValueConverter.NonEmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty NonEmptyValueProperty =
            DependencyProperty.Register("NonEmptyValue", typeof(bool?), typeof(StringToBooleanConverter), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="Erwine.Leonard.T.Examples.WpfExamples.Converters.StringToBoolValueConverter.EmptyValue"/> property.
        /// </summary>
        public static readonly DependencyProperty EmptyValueProperty =
            DependencyProperty.Register("EmptyValue", typeof(bool?), typeof(StringToBooleanConverter), new PropertyMetadata(false));

        /// <summary>
        /// <see cref="Nullable&lt;System.Boolean&gt;"/> value to use for non-empty strings
        /// </summary>
        public override bool? NonEmptyValue
        {
            get { return (bool?)(this.GetValue(StringToBooleanConverter.NonEmptyValueProperty)); }
            set { this.SetValue(StringToBooleanConverter.NonEmptyValueProperty, value); }
        }

        /// <summary>
        /// <see cref="Nullable&lt;System.Boolean&gt;"/> value to use for empty strings
        /// </summary>
        public override bool? EmptyValue
        {
            get { return (bool?)(this.GetValue(StringToBooleanConverter.EmptyValueProperty)); }
            set { this.SetValue(StringToBooleanConverter.EmptyValueProperty, value); }
        }
    }
}
