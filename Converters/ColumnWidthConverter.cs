using System.Globalization;
using System.Windows.Data;

namespace Copier.Converters
{
    public class ColumnWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double totalWidth && parameter is int columnCount && columnCount > 0)
            {
                return totalWidth / columnCount;
            }
            return 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}