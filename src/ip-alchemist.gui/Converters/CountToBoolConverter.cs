using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Extensions;
using System.Globalization;

namespace ip_alchemist.gui.Converters
{
    class CountToBoolConverter : ICommunityToolkitValueConverter
    {
        public object DefaultConvertReturnValue => throw new NotImplementedException();

        public object DefaultConvertBackReturnValue => throw new NotImplementedException();

        public Type FromType => typeof(int);

        public Type ToType => typeof(bool);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
