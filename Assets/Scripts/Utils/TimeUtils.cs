using System;
using System.Globalization;
using System.Runtime.InteropServices; // For OS platform detection

namespace Utils
{
    public static class TimeUtils
    {
        public static string ConvertToRfc3339(Time24H time24H)
        {
            return ConvertToRfc3339(time24H.hour - 1, time24H.minute);
        }

        public static string ConvertToRfc3339(int hour, int minute)
        {
            DateTime today = DateTime.UtcNow;

            DateTime utcDateTime = new DateTime(today.Year, today.Month, today.Day, hour, minute, 0, DateTimeKind.Utc);

            TimeSpan standardOffset = TimeSpan.FromHours(1); // Sweden Standard Time (UTC+1)
            TimeSpan dstOffset = TimeSpan.FromHours(2); // Sweden Daylight Saving Time (UTC+2)

            TimeSpan offset;
            DateTime transitionDate =
                new DateTime(today.Year, 3, GetLastSundayOfMonth(3), 1, 0, 0); // Last Sunday of March
            if (today >= transitionDate && today < transitionDate.AddMonths(7))
            {
                // Daylight Saving Time is in effect (from the last Sunday of March to the last Sunday of October)
                offset = dstOffset;
            }
            else
            {
                // Standard time is in effect
                offset = standardOffset;
            }

            string formattedOffset = offset.ToString(@"hh\:mm");
            if (offset.Hours >= 0)
            {
                formattedOffset = "+" + formattedOffset; // Ensure the plus sign for positive offsets
            }

            // Format the DateTime object to RFC-3339 format with the actual offset for Sweden time zone
            DateTime swedenDateTime = utcDateTime.Add(offset);
            string rfc3339String = swedenDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture) +
                                   formattedOffset;

            return rfc3339String;
        }

        private static int GetLastSundayOfMonth(int month)
        {
            DateTime lastDayOfMonth =
                new DateTime(DateTime.Now.Year, month, DateTime.DaysInMonth(DateTime.Now.Year, month));
            int lastSunday = lastDayOfMonth.Day - (int) lastDayOfMonth.DayOfWeek;
            return lastSunday > 0 ? lastSunday : lastSunday + 7;
        }
    }
}
