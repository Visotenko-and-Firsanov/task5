using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MessengerClient
{
    public sealed class BackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)values[0];

            if (values.Length > 2 && values[2] != DependencyProperty.UnsetValue)
            {
                var list2 = (List<string>)values[2];

                if (list2.Contains(item.Content))
                    return Brushes.Orange;
            }


            var list = new List<string>();

            if (values[1] != DependencyProperty.UnsetValue)
                list = (List<string>)values[1];


            if (list.Contains(item.Content))
                return Brushes.Chartreuse;

            return Brushes.AntiqueWhite;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}