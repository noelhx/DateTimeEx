using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Strange1.Utility.DateTimeExtensions
{
    public static class DateTimeExtensions
    {
        private static GregorianCalendar _gc = new GregorianCalendar();

        static DateTimeExtensions()
        {
        }

        public static DateTime RoundToHour(this DateTime d)
        {
            return d.Round(TimeSpan.FromHours(1));
        }

        public static DateTime RoundToDay(this DateTime d)
        {
            return d.Round(TimeSpan.FromDays(1));
        }

        public static DateTime RoundToMinute(this DateTime d)
        {
            return d.Round(TimeSpan.FromMinutes(1));
        }

        public static DateTime RoundToSecond(this DateTime d)
        {
            return d.Round(TimeSpan.FromSeconds(1));
        }

        public static DateTime Round(this DateTime d, TimeSpan t)
        {
            long ticks = (d.Ticks + (t.Ticks / 2) + 1) / t.Ticks;
            return new DateTime(ticks * t.Ticks, d.Kind);
        }

        public static DateTime RoundUp(this DateTime d, TimeSpan t)
        {
            long ticks = (d.Ticks + t.Ticks - 1) / t.Ticks;
            return new DateTime(ticks * t.Ticks, d.Kind);
        }

        public static DateTime RoundDown(this DateTime d, TimeSpan t)
        {
            long ticks = d.Ticks / t.Ticks;
            return new DateTime(ticks * t.Ticks, d.Kind);
        }

        public static DateTime Round(this DateTime d, RoundTo rt)
        {
            DateTime dtRounded = new DateTime();

            switch (rt)
            {
                case RoundTo.Second:
                    dtRounded = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second, d.Kind);
                    if (d.Millisecond >= 500) dtRounded = dtRounded.AddSeconds(1);
                    break;

                case RoundTo.Minute:
                    dtRounded = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0, d.Kind);
                    if (d.Second >= 30) dtRounded = dtRounded.AddMinutes(1);
                    break;

                case RoundTo.QuarterHour:
                    dtRounded = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0, d.Kind);
                    if (d.Second >= 30) dtRounded = dtRounded.AddMinutes(1);
                    break;

                case RoundTo.HalfHour:
                    dtRounded = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0, d.Kind);
                    if (d.Second >= 30) dtRounded = dtRounded.AddMinutes(1);
                    break;

                case RoundTo.Hour:
                    dtRounded = new DateTime(d.Year, d.Month, d.Day, d.Hour, 0, 0, d.Kind);
                    if (d.Minute >= 30) dtRounded = dtRounded.AddHours(1);
                    break;

                case RoundTo.Day:
                    dtRounded = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0, d.Kind);
                    if (d.Hour >= 12) dtRounded = dtRounded.AddDays(1);
                    break;

                case RoundTo.Month:
                    dtRounded = new DateTime(d.Year, d.Month, 1, 0, 0, 0, d.Kind);
                    if (d.Day >= (d.DaysInMonth() / 2))
                    {
                        dtRounded = dtRounded.AddDays(d.DaysInMonth());
                    }
                    break;
            }

            return dtRounded;
        }

        public static int DaysInMonth(this DateTime d)
        {
            return d.EndOfMonth().Day;
        }

        public enum RoundTo
        {
            Second,
            Minute,
            QuarterHour,
            HalfHour,
            Hour,
            Day,
            Month
        }

        public static int WeekdayOccurrence(this DateTime time)
        {
            return Enumerable.Range(1, time.Day)
                .Where(d => new DateTime(time.Year, time.Month, d).DayOfWeek == time.DayOfWeek).Count();
        }

        public static int WeekOfYear(this DateTime time, DayOfWeek dayOfWeek = System.DayOfWeek.Sunday)
        {
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, dayOfWeek);
        }

        public static string OrdinalSuffix(this int value)
        {
            string suffix = "th";

            int ones = value % 10;
            int tens = (int)(Math.Floor((double)(value / 10)) % 10);
            if (tens == 1)
            {
                suffix = "th";
            }
            else
            {
                switch (ones)
                {
                    case 1:
                        suffix = "st";
                        break;

                    case 2:
                        suffix = "nd";
                        break;

                    case 3:
                        suffix = "rd";
                        break;

                    default:
                        suffix = "th";
                        break;
                }
            }
            return suffix;
        }

        public static string Ordinal(this int value)
        {
            string suffix = "th";

            int ones = value % 10;
            int tens = (int)(Math.Floor((double)(value / 10)) % 10);
            if (tens == 1)
            {
                suffix = "th";
            }
            else
            {
                switch (ones)
                {
                    case 1:
                        suffix = "st";
                        break;

                    case 2:
                        suffix = "nd";
                        break;

                    case 3:
                        suffix = "rd";
                        break;

                    default:
                        suffix = "th";
                        break;
                }
            }
            return string.Format("{0}{1}", value, suffix);
        }

        public static DateTime AddTimespan(this DateTime datetime, TimeSpan timespan)
        {
            return datetime.AddTicks(timespan.Ticks);
        }

        public static DateTime StartOfHour(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, 0, 0);
        }

        public static bool IsGreaterThan(this DateTime dateTime, DateTime dateTimeToCompare)
        {
            return dateTime.ToUniversalTime().CompareTo(dateTimeToCompare.ToUniversalTime()) > 0;
        }

        public static bool IsLessThan(this DateTime dateTime, DateTime dateTimeToCompare)
        {
            return dateTime.ToUniversalTime().CompareTo(dateTimeToCompare.ToUniversalTime()) < 0;
        }

        public static bool IsEqualTo(this DateTime dateTime, DateTime dateTimeToCompare)
        {
            return dateTime.ToUniversalTime().CompareTo(dateTimeToCompare.ToUniversalTime()) == 0;
        }

        public static bool IsGreaterThanOrEqualTo(this DateTime dateTime, DateTime dateTimeToCompare)
        {
            return dateTime.ToUniversalTime().CompareTo(dateTimeToCompare.ToUniversalTime()) >= 0;
        }

        public static bool IsLessThanOrEqualTo(this DateTime dateTime, DateTime dateTimeToCompare)
        {
            return dateTime.ToUniversalTime().CompareTo(dateTimeToCompare.ToUniversalTime()) <= 0;
        }

        /// <summary>
        /// returns true if the dateTime falls inside the time period
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timePeriod"></param>
        /// <returns></returns>
        public static bool Between(this DateTime dateTime, TimePeriod timePeriod)
        {
            return dateTime.ToUniversalTime().IsGreaterThanOrEqualTo(timePeriod.UtcStartTime) && dateTime.ToUniversalTime().IsLessThanOrEqualTo(timePeriod.UtcEndTime);
        }

        public static int CompareTo(this DateTime dateTime, TimePeriod timePeriod)
        {
            // does the date time fall inside the time period?
            if (dateTime.Between(timePeriod))
            {
                return 0;
            }
            // is the date time before the time period?
            else if (dateTime.IsLessThan(timePeriod.UtcStartTime))
            {
                return -1;
            }
            // if we are not inside or before the time period, we must be after the time period
            return 1;
        }

        public static bool IsWeekend(this DayOfWeek d)
        {
            return !d.IsWeekday();
        }

        public static bool IsWeekday(this DayOfWeek d)
        {
            switch (d)
            {
                case System.DayOfWeek.Sunday:
                case System.DayOfWeek.Saturday: return false;

                default: return true;
            }
        }

        public static DateTime AddWorkdays(this DateTime d, int days)
        {
            // start from a weekday
            while (d.DayOfWeek.IsWeekday()) d = d.AddDays(1.0);
            for (int i = 0; i < days; ++i)
            {
                d = d.AddDays(1.0);
                while (d.DayOfWeek.IsWeekday()) d = d.AddDays(1.0);
            }
            return d;
        }

        public static string SqlDataTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a System.DateTime object to Unix timestamp
        /// </summary>
        /// <returns>The Unix timestamp</returns>
        public static long ToEpochTime(this DateTime date)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan unixTimeSpan = date.ToUniversalTime() - unixEpoch;

            return (long)unixTimeSpan.TotalSeconds;
        }

        public static DateTime ToDateTime(this long epochSeconds)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixEpoch.AddSeconds(epochSeconds);
        }

        public static double ToExcelSerialDate(this DateTime date)
        {
            return date.ToOADate();
        }

        public static DateTime ExcelSerialDateToDateTime(this double serialDate)
        {
            DateTime result = DateTime.MinValue.ToUniversalTime();
            try
            {
                result = DateTime.FromOADate(serialDate);
            }
            catch (Exception)
            {
                string message = string.Format("{0} is not a valid Excel serial date", serialDate);
                throw new System.ArgumentException(message);
            }
            return result;
        }

        /// <summary>
        ///  returns the current day
        /// </summary>
        public static DateTime Today(this DateTime date)
        {
            DateTime dt = date.ForceLocalTimeIfUnspecified();
            int seconds = (int)dt.Second + ((int)dt.Minute * 60) + ((int)dt.Hour * 60 * 60);
            return dt.Subtract(TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// returns the number of minutes since midnight
        /// </summary>
        public static double MinutesPastMidnight(this DateTime date)
        {
            return date.ForceLocalTimeIfUnspecified().Minute + (date.ForceLocalTimeIfUnspecified().Hour * 60);
        }

        /// <summary>
        /// returns the second of the day for the requested date
        /// </summary>
        public static double SecondsPastMidnight(this DateTime date)
        {
            return date.ForceLocalTimeIfUnspecified().Second + (date.ForceLocalTimeIfUnspecified().Minute * 60) + (date.ForceLocalTimeIfUnspecified().Hour * 60 * 60);
        }

        /// <summary>
        /// returns the current day of the week
        /// </summary>
        public static string DayOfWeekName(this DateTime date)
        {
            return DateTimeFormatInfo.InvariantInfo.GetDayName(date.ForceLocalTimeIfUnspecified().DayOfWeek);
        }

        /// <summary/>
        public static string ShortDayOfWeekName(this DateTime date)
        {
            return DateTimeFormatInfo.InvariantInfo.GetShortestDayName(date.ForceLocalTimeIfUnspecified().DayOfWeek);
        }

        /// <summary>
        /// returns the ordinal day of week: 1 (Sunday) thru 7(Saturday)
        /// </summary>
        public static double DayOfWeek(this DateTime date)
        {
            return (double)date.ForceLocalTimeIfUnspecified().DayOfWeek + 1;
        }

        /// <summary>
        /// returns the current day of the year
        /// </summary>
        public static double DayOfYear(this DateTime date)
        {
            return (double)date.ForceLocalTimeIfUnspecified().DayOfYear;
        }

        /// <summary>
        /// returns true if the date is on a weekend
        /// </summary>
        public static bool IsWeekEnd(this DateTime date)
        {
            return (int)date.ForceLocalTimeIfUnspecified().DayOfWeek == 0 || (int)date.ForceLocalTimeIfUnspecified().DayOfWeek == 6;
        }

        /// <summary>
        /// returns true if the date is on a weekday
        /// </summary>
        public static bool IsWeekDay(this DateTime date)
        {
            return !(IsWeekEnd(date.ForceLocalTimeIfUnspecified()));
        }

        /// <summary>
        /// returns the start of the previous week as a date
        /// </summary>
        public static DateTime StartOfPreviousWeek(this DateTime date)
        {
            DateTime checkDate = date.ForceLocalTimeIfUnspecified();
            int DaysToSubtract = (int)checkDate.DayOfWeek + 7;
            DateTime dt = checkDate.Subtract(System.TimeSpan.FromDays(DaysToSubtract));
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// returns the end of last week as a date
        /// </summary>
        public static DateTime EndOfPreviousWeek(this DateTime date)
        {
            DateTime checkDate = date.ForceLocalTimeIfUnspecified();
            DateTime dt = checkDate.StartOfPreviousWeek().AddDays(6);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// returns the start of this week as a date
        /// </summary>
        public static DateTime StartOfWeek(this DateTime date)
        {
            DateTime checkDate = date.ForceLocalTimeIfUnspecified();
            int DaysToSubtract = (int)checkDate.DayOfWeek;
            DateTime dt = checkDate.Subtract(System.TimeSpan.FromDays(DaysToSubtract));
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// returns the end of the requested week as a date
        /// </summary>
        public static DateTime EndOfWeek(this DateTime date)
        {
            DateTime dt = date.StartOfWeek().AddDays(6);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// returns the timestamp that starts the month
        /// </summary>
        public static DateTime StartOfMonth(this DateTime date)
        {
            DateTime dt = date.ForceLocalTimeIfUnspecified();
            return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// returns the timestamp that ends the month
        /// </summary>
        public static DateTime EndOfMonth(this DateTime date)
        {
            DateTime dt = date.ForceLocalTimeIfUnspecified();
            return new DateTime(dt.Year, dt.Month,
               DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59, 999);
        }

        /// <summary>
        /// returns the timestamp that starts the previous month
        /// </summary>
        public static DateTime StartOfPreviousMonth(this DateTime date)
        {
            DateTime dt = date.ForceLocalTimeIfUnspecified();
            if (dt.Month == 1)
            {
                return new DateTime(dt.Year - 1, 12, 1, 0, 0, 0);
            }
            else
            {
                return new DateTime(dt.Year, dt.Month - 1, 1, 0, 0, 0);
            }
        }

        /// <summary>
        /// returns the timestamp that ends the previous month
        /// </summary>
        public static DateTime EndOfPreviousMonth(this DateTime date)
        {
            DateTime dt = date.ForceLocalTimeIfUnspecified();
            // first of current month
            DateTime start = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
            // subtract one second
            TimeSpan ts = new TimeSpan(0, 0, 1);
            DateTime end = start.Subtract(ts);
            return end;
        }

        /// <summary>
        /// returns the timestamp of the start of the date
        /// </summary>
        public static DateTime StartOfDay(this DateTime date)
        {
            DateTime dt = date.ForceLocalTimeIfUnspecified();
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// returns the timestamp that starts the previous day
        /// </summary>
        public static DateTime StartOfPreviousDay(this DateTime date)
        {
            return StartOfDay(date.ForceLocalTimeIfUnspecified()).AddDays(-1);
        }

        /// <summary>
        /// returns the timestamp that ends the date
        /// </summary>
        public static DateTime EndOfDay(this DateTime date)
        {
            DateTime dt = date.ForceLocalTimeIfUnspecified();
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// returns the timestamp of the end of the previous day
        /// </summary>
        public static DateTime EndOfPreviousDay(this DateTime date)
        {
            return date.ForceLocalTimeIfUnspecified().AddDays(-1).EndOfDay();
        }

        /// <summary>
        /// returns the number of days since the date
        /// </summary>
        public static double AgeInDays(this DateTime date)
        {
            TimeSpan ts = TimeSpanSince(date.ForceLocalTimeIfUnspecified());
            return ts.TotalDays;
        }

        /// <summary>
        /// returns the number of hours since the date
        /// </summary>
        public static double AgeInHours(this DateTime date)
        {
            TimeSpan ts = TimeSpanSince(date.ForceLocalTimeIfUnspecified());
            return ts.TotalHours;
        }

        /// <summary>
        /// returns the number of minutes since the date
        /// </summary>
        public static double AgeInMinutes(this DateTime date)
        {
            TimeSpan ts = TimeSpanSince(date.ForceLocalTimeIfUnspecified());
            return ts.TotalMinutes;
        }

        /// <summary>
        /// returns the number of seconds since the date
        /// </summary>
        public static double AgeInSeconds(this DateTime date)
        {
            TimeSpan ts = TimeSpanSince(date.ForceLocalTimeIfUnspecified());
            return ts.TotalSeconds;
        }

        public static TimeSpan TimeSpanSince(this DateTime date)
        {
            DateTime dt = date.ForceLocalTimeIfUnspecified();
            DateTime now = DateTime.Now.ToLocalTime();
            return now.Subtract(dt);
        }

        /// <summary>
        /// given two dates, return the difference in seconds
        /// </summary>
        public static double DiffInSeconds(this DateTime date1, DateTime date2)
        {
            TimeSpan ts = date1.Subtract(date2);
            return ts.TotalSeconds;
        }

        /// <summary>
        /// given two dates, return the difference in minutes
        /// </summary>
        public static double DiffInMinutes(this DateTime date1, DateTime date2)
        {
            TimeSpan ts = date1.Subtract(date2);
            return ts.TotalMinutes;
        }

        /// <summary>
        /// given two dates, return the difference in hours
        /// </summary>
        public static double DiffInHours(this DateTime date1, DateTime date2)
        {
            TimeSpan ts = date1.Subtract(date2);
            return ts.TotalHours;
        }

        /// <summary>
        /// given two dates, return the difference in days
        /// </summary>
        public static double DiffInDays(this DateTime date1, DateTime date2)
        {
            TimeSpan ts = date1.Subtract(date2);
            return ts.TotalDays;
        }

        /// <summary/>
        public static DateTime StartOfYear(this DateTime date)
        {
            return new DateTime(date.ForceLocalTimeIfUnspecified().Year, 1, 1, 0, 0, 0);
        }

        private static bool ParseStringToDateTime(string date, out DateTime parsedDateTime)
        {
            return DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out parsedDateTime);
        }

        public static DateTime EndOfYear(this DateTime date)
        {
            return new DateTime(date.ForceLocalTimeIfUnspecified().Year, 12, 31, 23, 59, 59);
        }

        /// <summary/>
        public static string MonthName(this DateTime date)
        {
            //return  date.ToString("MMMM");
            return DateTimeFormatInfo.InvariantInfo.GetMonthName(date.ForceLocalTimeIfUnspecified().Month);
        }

        /// <summary>
        /// returns the short month name, according to the server's localization settings
        /// </summary>
        public static string ShortMonthName(this DateTime date)
        {
            return DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(date.ForceLocalTimeIfUnspecified().Month);
        }

        /// <summary>
        /// returns the passed in <see cref="DateTime"/> converted to Server LocalTime
        /// </summary>
        public static DateTime ForceLocalTimeIfUnspecified(this DateTime date)
        {
            // handle date from string with unspecified Kind
            if (date.Kind == DateTimeKind.Unspecified)
            {
                // if we do not explicitly convert this to local time
                // results will not be as expected
                return DateTime.SpecifyKind(date, DateTimeKind.Local); // new DateTime(date.Ticks, DateTimeKind.Local);
            }
            //else if (date.Kind == DateTimeKind.Utc)
            //{
            //    return date.ToLocalTime();
            //}
            // return the already local time
            return date;
        }

        public static IEnumerable<DateTime> GetAllDates(this DateTime lhs, DateTime futureDate)
        {
            List<DateTime> dateRange = new List<DateTime>();
            TimeSpan difference = (futureDate - lhs);
            for (int i = 0; i <= difference.Days; i++)
            {
                dateRange.Add(lhs.AddDays(i));
            }
            return dateRange;
        }

        public static IEnumerable<DateTime> GetAllWeekDays(this DateTime lhs, DateTime futureDate)
        {
            List<DateTime> dateRange = new List<DateTime>();
            TimeSpan difference = (futureDate - lhs);
            for (int i = 0; i <= difference.Days; i++)
            {
                DateTime newDate = lhs.AddDays(i);
                if (newDate.DayOfWeek != System.DayOfWeek.Saturday && newDate.DayOfWeek != System.DayOfWeek.Sunday)
                {
                    dateRange.Add(newDate);
                }
            }
            return dateRange;
        }

        public static IEnumerable<DateTime> GetAllWeekEndDays(this DateTime lhs, DateTime futureDate)
        {
            List<DateTime> dateRange = new List<DateTime>();
            TimeSpan difference = (futureDate - lhs);
            for (int i = 0; i <= difference.Days; i++)
            {
                DateTime newDate = lhs.AddDays(i);
                if (newDate.DayOfWeek == System.DayOfWeek.Saturday || newDate.DayOfWeek == System.DayOfWeek.Sunday)
                {
                    dateRange.Add(newDate);
                }
            }
            return dateRange;
        }

        public static IEnumerable<DateTime> GetAllDays(this DateTime lhs, DateTime futureDate, DayOfWeek dayOfWeek)
        {
            List<DateTime> dateRange = new List<DateTime>();
            TimeSpan difference = (futureDate - lhs);
            for (int i = 0; i <= difference.Days; i++)
            {
                DateTime newDate = lhs.AddDays(i);
                if (newDate.DayOfWeek == dayOfWeek)
                {
                    dateRange.Add(newDate);
                }
            }
            return dateRange;
        }

        public static int CountAllWeekDays(this DateTime lhs, DateTime futureDate)
        {
            return lhs.GetAllWeekDays(futureDate).Count();
        }

        public static int CountAllWeekEndDays(this DateTime lhs, DateTime futureDate)
        {
            return lhs.GetAllWeekEndDays(futureDate).Count();
        }

        public static int CountAllDays(this DateTime lhs, DateTime futureDate, DayOfWeek dayOfWeek)
        {
            return lhs.GetAllDays(futureDate, dayOfWeek).Count();
        }

        public static IEnumerable<TimePeriod> GetAllTimePeriods(this DateTime lhs, DateTime futureDate, int intervalMinutes)
        {
            List<TimePeriod> dateRange = new List<TimePeriod>();
            TimeSpan difference = (futureDate - lhs);
            for (int i = 0; i < difference.TotalMinutes; i += intervalMinutes)
            {
                dateRange.Add(new TimePeriod(lhs.AddMinutes(i), lhs.AddMinutes(i + intervalMinutes)));
            }
            return dateRange;
        }
    }
}