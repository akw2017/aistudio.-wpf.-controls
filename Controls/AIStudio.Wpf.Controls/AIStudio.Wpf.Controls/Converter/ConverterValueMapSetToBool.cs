﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace AIStudio.Wpf.Controls.Converter
{
    public class ConverterValueMapSetToBool : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || parameter == null)
                return DependencyProperty.UnsetValue;

            IEnumerable<string> checkList = parameter.ToString().Split('^');

            bool equal = checkList.Contains(value.ToString());

            if (equal)
                return !IsOpposite;
            else
                return IsOpposite;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ConverterValueMapSetToBool
            {
                FromType = this.FromType ?? typeof(char),
                TargetType = this.TargetType ?? typeof(bool),
                Parameter = this.Parameter,
                IsOpposite = this.IsOpposite
            };
        }

        public bool IsOpposite { get; set; }
        public object Parameter
        {
            get;
            set;
        }
        public Type TargetType
        {
            get;
            set;
        }
        public Type FromType
        {
            get;
            set;
        }
    }
}
