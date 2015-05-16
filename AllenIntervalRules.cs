using System;

namespace Strange1.Utility.DateTimeExtensions
{
    public static class AllenIntervalRules
    {
        public static bool Overlaps(this TimePeriod x, TimePeriod y)
        {
            return (x.UtcStartTime < y.UtcStartTime) && ((x.UtcEndTime > y.UtcStartTime) && (x.UtcEndTime < y.UtcEndTime));
        }

        public static bool OverlapedBy(this TimePeriod x, TimePeriod y)
        {
            return (y.UtcStartTime < x.UtcStartTime) && ((y.UtcEndTime > x.UtcStartTime) && (y.UtcEndTime < x.UtcEndTime));
        }

        public static bool TakesPlaceBefore(this TimePeriod x, TimePeriod y)
        {
            return x.UtcEndTime < y.UtcStartTime;
        }

        public static bool TakesPlaceAfter(this TimePeriod x, TimePeriod y)
        {
            return x.UtcStartTime > y.UtcEndTime;
        }

        public static bool Meets(this TimePeriod x, TimePeriod y)
        {
            return (x.UtcEndTime == y.UtcStartTime);
        }

        public static bool MetBy(this TimePeriod x, TimePeriod y)
        {
            return (y.UtcEndTime == x.UtcStartTime);
        }

        public static bool StartedBy(this TimePeriod x, TimePeriod y)
        {
            return (y.UtcStartTime == x.UtcStartTime) && (y.UtcEndTime < x.UtcEndTime);
        }

        public static bool Starts(this TimePeriod x, TimePeriod y)
        {
            return (x.UtcStartTime == y.UtcStartTime) && (x.UtcEndTime < y.UtcEndTime);
        }

        public static bool Contains(this TimePeriod x, TimePeriod y)
        {
            return (y.UtcStartTime > x.UtcStartTime) && (y.UtcEndTime < x.UtcEndTime);
        }

        public static bool ContainedBy(this TimePeriod x, TimePeriod y)
        {
            return (x.UtcStartTime > y.UtcStartTime) && (x.UtcEndTime < y.UtcEndTime);
        }

        public static bool FinishedBy(this TimePeriod x, TimePeriod y)
        {
            return (y.UtcEndTime == x.UtcEndTime) && (y.UtcStartTime > x.UtcStartTime);
        }

        public static bool Finishes(this TimePeriod x, TimePeriod y)
        {
            return (x.UtcEndTime == y.UtcEndTime) && (x.UtcStartTime > y.UtcStartTime);
        }

        public static bool IsEqualTo(this TimePeriod x, TimePeriod y)
        {
            return (x.UtcStartTime == y.UtcStartTime) && (x.UtcEndTime == y.UtcEndTime);
        }
    }
}
