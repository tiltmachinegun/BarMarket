using System;
using System.Globalization;
using System.Windows.Data;

namespace BarMarket.Views
{
    public class VolumeUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            {
                if (value is decimal volume)
                {
                    if (volume >= 1)
                    {
                        return $"{volume:N0} л";
                    }
                    else if (volume > 0)
                    {
                        return $"{volume*1000:N0} мл";
                    }
                }

                return "-";
            }
        }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}