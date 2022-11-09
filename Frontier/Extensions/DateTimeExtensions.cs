using System;

namespace Frontier.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero)
            {
                return dateTime;
            }

            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                return dateTime;
            }

            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }

        public static string ToIso(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");
        }

        public static string ToIsoDate(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy'-'MM'-'dd");
        }
    }
}