using System.Globalization;
using System.Windows.Data;

namespace Copier.Converters
{
    public class FolderButtonConverter : IValueConverter
    {
        private readonly string DefaultText = "*** SELECT FOLDER LOCATION ***";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string ? value : DefaultText;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string && value.Equals(DefaultText) ? null : value;
        }
    }
}