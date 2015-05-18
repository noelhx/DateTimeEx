using System;

namespace Strange1.Utility.DateTimeExtensions
{
    /// <summary>
    /// see https://www.ics.uci.edu/~alspaugh/cls/shr/allen.html
    /// </summary>
    public static class AllensIntervalAlgebra
    {
        public static bool Overlaps(this TimePeriod a, TimePeriod b)
        {
            return (a.UtcStartTime < b.UtcStartTime) && ((a.UtcEndTime > b.UtcStartTime) && (a.UtcEndTime < b.UtcEndTime));
        }

        public static bool OverlapedBy(this TimePeriod a, TimePeriod b)
        {
            return (b.UtcStartTime < a.UtcStartTime) && ((b.UtcEndTime > a.UtcStartTime) && (b.UtcEndTime < a.UtcEndTime));
        }

        public static bool Precedes(this TimePeriod a, TimePeriod b)
        {
            return a.UtcEndTime < b.UtcStartTime && ((b.UtcStartTime - a.UtcEndTime).TotalMilliseconds > 0);
        }

        public static bool PrecededBy(this TimePeriod a, TimePeriod b)
        {
            return a.UtcStartTime > b.UtcEndTime && ((a.UtcStartTime - b.UtcEndTime).TotalMilliseconds > 0);
        }

        public static bool Meets(this TimePeriod a, TimePeriod b)
        {
            return (a.UtcEndTime == b.UtcStartTime);
        }

        public static bool MetBy(this TimePeriod a, TimePeriod b)
        {
            return (b.UtcEndTime == a.UtcStartTime);
        }

        public static bool StartedBy(this TimePeriod a, TimePeriod b)
        {
            return (b.UtcStartTime == a.UtcStartTime) && (b.UtcEndTime < a.UtcEndTime);
        }

        public static bool Starts(this TimePeriod a, TimePeriod b)
        {
            return (a.UtcStartTime == b.UtcStartTime) && (a.UtcEndTime < b.UtcEndTime);
        }

        public static bool Contains(this TimePeriod a, TimePeriod b)
        {
            return (b.UtcStartTime > a.UtcStartTime) && (b.UtcEndTime < a.UtcEndTime);
        }

        public static bool During(this TimePeriod a, TimePeriod b)
        {
            return (a.UtcStartTime > b.UtcStartTime) && (a.UtcEndTime < b.UtcEndTime);
        }

        public static bool FinishedBy(this TimePeriod a, TimePeriod b)
        {
            return (b.UtcEndTime == a.UtcEndTime) && (b.UtcStartTime > a.UtcStartTime);
        }

        public static bool Finishes(this TimePeriod a, TimePeriod b)
        {
            return (a.UtcEndTime == b.UtcEndTime) && (a.UtcStartTime > b.UtcStartTime);
        }

        public static bool IsEqualTo(this TimePeriod a, TimePeriod b)
        {
            return (a.UtcStartTime == b.UtcStartTime) && (a.UtcEndTime == b.UtcEndTime);
        }
    }
}
