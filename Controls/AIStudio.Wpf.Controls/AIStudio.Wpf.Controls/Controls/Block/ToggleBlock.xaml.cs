﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AIStudio.Wpf.Controls
{
    /// <summary>
    ///     切换块
    /// </summary>
    /// <remarks>
    ///     在不需要命中测试的情况下可代替ToggleButton或CheckBox，以减少内存用量。
    /// </remarks>
    public class ToggleBlock : Control
    {
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
            nameof(IsChecked), typeof(bool?), typeof(ToggleBlock), new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));

        [Category("Appearance")]
        [TypeConverter(typeof(NullableBoolConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public bool? IsChecked
        {
            get
            {
                // Because Nullable<bool> unboxing is very slow (uses reflection) first we cast to bool
                var value = GetValue(IsCheckedProperty);
                // ReSharper disable once RedundantExplicitNullableCreation
                return value == null ? new bool?() : new bool?((bool)value);
            }
            set => SetValue(IsCheckedProperty, value);
        }

        public static readonly DependencyProperty CheckedContentProperty = DependencyProperty.Register(
            nameof(CheckedContent), typeof(object), typeof(ToggleBlock), new PropertyMetadata(default(object)));

        public object CheckedContent
        {
            get => GetValue(CheckedContentProperty);
            set => SetValue(CheckedContentProperty, value);
        }

        public static readonly DependencyProperty UnCheckedContentProperty = DependencyProperty.Register(
            nameof(UnCheckedContent), typeof(object), typeof(ToggleBlock), new PropertyMetadata(default(object)));

        public object UnCheckedContent
        {
            get => GetValue(UnCheckedContentProperty);
            set => SetValue(UnCheckedContentProperty, value);
        }

        public static readonly DependencyProperty IndeterminateContentProperty = DependencyProperty.Register(
            nameof(IndeterminateContent), typeof(object), typeof(ToggleBlock), new PropertyMetadata(default(object)));

        public object IndeterminateContent
        {
            get => GetValue(IndeterminateContentProperty);
            set => SetValue(IndeterminateContentProperty, value);
        }
    }
}