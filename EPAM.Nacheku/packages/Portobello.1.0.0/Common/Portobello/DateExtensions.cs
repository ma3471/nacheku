using System;

namespace Expoware.Portobello.Extensions
{
    public static class DateExtensions
    {
        /// <summary>
        /// Given a date, it returns the next (specified) day of week 
        /// </summary>
        /// <param name="date">Date to process</param>
        /// <param name="day">Day of week to find on calendar</param>
        /// <returns>Future date</returns>
        public static DateTime NextDayOfWeek(this DateTime date, DayOfWeek day = DayOfWeek.Monday)
        {
            while (true)
            {
                if (date.DayOfWeek == day)
                    return date;
                date = date.AddDays(1);
            }
        }

        /// <summary>
        /// Given a date, it returns the next (specified) day of week
        /// </summary>
        /// <param name="date">Date to process</param>
        /// <param name="timezoneFromUtc">Hours of the timezone from UTC</param>
        /// <returns>Future date</returns>
        public static DateTime LocalTimeFromUtc(this DateTime date, Int32 timezoneFromUtc)
        {
            return date.ToUniversalTime().AddHours(timezoneFromUtc);
        }
    }
}