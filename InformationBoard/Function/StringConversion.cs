using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InformationBoard.Function
{
    public class CityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values[1].ToString() == "0")
            {
                return String.Format("{0} - Пермь", values[0]);
            }
            else
            {   
                return String.Format("Пермь - {0}", values[0]);
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var size = (Size)value;
            return new object[] { size.Width, size.Height };
        }
    }
}
