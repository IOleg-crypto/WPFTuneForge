using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfTuneForgePlayer
{
    public class BoolToStarColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b && b) ? Brushes.Gold : Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
