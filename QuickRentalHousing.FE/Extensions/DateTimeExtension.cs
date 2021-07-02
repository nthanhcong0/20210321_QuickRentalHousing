using System;

namespace QuickRentalHousing.FE.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToString_ddMMyyyy(this DateTime dateTime,
            string separator = "/")
        {
            var format = $"dd{separator}MM{separator}yyyy";

            return dateTime.ToString(format);
        }
    }
}
