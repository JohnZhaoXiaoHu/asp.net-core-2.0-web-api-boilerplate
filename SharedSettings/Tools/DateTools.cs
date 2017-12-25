using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SharedSettings.Tools
{
    public class DateTools
    {
        public static string OrderDateFormat => "yyyy-MM-dd";

        public DateTime GetDateTimeFromString(string dateString, string format)
        {
            DateTime result;
            result = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            return result;
        }
    }
}
