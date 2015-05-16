using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strange1.Utility.DateTimeExtensions
{
    public static class TimePeriodExtensions
    {
        private static TimePeriodExtensions() { }

        public static IEnumerable<DateTime> GetAllDates(this TimePeriod timePeriod)
        {
            return timePeriod.StartTime.GetAllDates(timePeriod.EndTime);
        }

        public static IEnumerable<DateTime> GetAllWeekDays(this TimePeriod timePeriod)
        {
            return timePeriod.StartTime.GetAllWeekDays(timePeriod.EndTime);
        }
        
        public static IEnumerable<DateTime> GetAllWeekEndDays(this TimePeriod timePeriod)
        {
            return timePeriod.StartTime.GetAllWeekEndDays(timePeriod.EndTime);
        }
        
        public static IEnumerable<TimePeriod> GetAllDates(this TimePeriod timePeriod, int intervalMinutes)
        {
            return timePeriod.StartTime.GetAllTimePeriods(timePeriod.EndTime, intervalMinutes);
        }
        
        public static int CountAllWeekDays(this TimePeriod timeperiod)
        {
            return timeperiod.StartTime.CountAllWeekDays(timeperiod.EndTime);
        }
        
        public static IEnumerable<DateTime> GetAllDays(this TimePeriod timeperiod, DayOfWeek dayOfWeek)
        {
            return timeperiod.StartTime.GetAllDays(timeperiod.EndTime, dayOfWeek);
        }

        public static int CountAllDays(this TimePeriod timeperiod, DayOfWeek dayOfWeek)
        {
            return timeperiod.StartTime.CountAllDays(timeperiod.EndTime, dayOfWeek);
        }
    }
}
