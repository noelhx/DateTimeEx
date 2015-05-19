using System;
using System.Collections.Generic;

namespace Strange1.Utility.DateTimeExtensions
{
    public static class IntervalExtensions
    {
        #region Constructors

        #region Static Constructors

        static IntervalExtensions()
        {
        }

        #endregion Static Constructors

        #endregion Constructors

        #region Methods

        /// <summary>
        /// the count of the specified day of the week in the time period
        /// </summary>
        /// <param name="timeperiod">time period to process</param>
        /// <param name="dayOfWeek">DayOfWeek type to count</param>
        /// <returns>the number of occurrences of the DayOfWeek in the time period</returns>
        public static int CountAllDays(this Interval timeperiod, DayOfWeek dayOfWeek)
        {
            return timeperiod.StartTime.CountAllDays(timeperiod.EndTime, dayOfWeek);
        }

        /// <summary>
        /// the number of total weekdays in the time period
        /// </summary>
        /// <param name="timeperiod"></param>
        /// <returns></returns>
        public static int CountAllWeekDays(this Interval timeperiod)
        {
            return timeperiod.StartTime.CountAllWeekDays(timeperiod.EndTime);
        }

        public static IEnumerable<DateTime> GetAllDates(this Interval timePeriod)
        {
            return timePeriod.StartTime.GetAllDates(timePeriod.EndTime);
        }

        public static IEnumerable<Interval> GetAllDates(this Interval timePeriod, int intervalMinutes)
        {
            return timePeriod.StartTime.GetAllTimePeriods(timePeriod.EndTime, intervalMinutes);
        }

        public static IEnumerable<DateTime> GetAllDays(this Interval timeperiod, DayOfWeek dayOfWeek)
        {
            return timeperiod.StartTime.GetAllDays(timeperiod.EndTime, dayOfWeek);
        }

        public static IEnumerable<DateTime> GetAllWeekDays(this Interval timePeriod)
        {
            return timePeriod.StartTime.GetAllWeekDays(timePeriod.EndTime);
        }

        public static IEnumerable<DateTime> GetAllWeekEndDays(this Interval timePeriod)
        {
            return timePeriod.StartTime.GetAllWeekEndDays(timePeriod.EndTime);
        }

        #endregion Methods
    }
}