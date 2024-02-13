using System;
using System.Globalization;
using System.Runtime.InteropServices; // For OS platform detection

namespace Utils
{
    public static class TimeUtils
    {
        public static string ConvertToRfc3339(Time24H time24H)
        {
            return ConvertToRfc3339(time24H.hour, time24H.minute);
        }

        public static string ConvertToRfc3339(int hour, int minute)
        {
            // Get today's date in UTC
            DateTime today = DateTime.UtcNow;

            // Create a new DateTime object in UTC with today's date and specified hour and minute
            DateTime utcDateTime = new DateTime(today.Year, today.Month, today.Day, hour, minute, 0, DateTimeKind.Utc);

            // Use different time zone IDs based on the operating system
            string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Central Europe Standard Time" : "Europe/Berlin";

            // Define the Central European Time Zone (CET, UTC+1 or UTC+2 depending on DST)
            TimeZoneInfo cetZone;
            try
            {
                cetZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine($"The time zone '{timeZoneId}' could not be found on the system.");
                return null; // or handle the error as appropriate for your application
            }

            // Convert the DateTime from UTC to CET
            DateTime cetDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, cetZone);

            // Automatically determine the offset for CET (considering Daylight Saving Time if applicable)
            TimeSpan offset = cetZone.GetUtcOffset(cetDateTime);
            string formattedOffset = offset.ToString(@"hh\:mm");
            if (offset.Hours >= 0)
            {
                formattedOffset = "+" + formattedOffset; // Ensure the plus sign for positive offsets
            }

            // Format the DateTime object to RFC-3339 format with the actual offset for CET
            string rfc3339String = cetDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture) + formattedOffset;

            return rfc3339String;
        }
    }

}
