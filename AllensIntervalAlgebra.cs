namespace Strange1.Utility.DateTimeExtensions
{
    /// <summary>
    /// see https://www.ics.uci.edu/~alspaugh/cls/shr/allen.html
    /// </summary>
    public static class AllensIntervalAlgebra
    {
        #region Enumerations

        /// <summary>
        /// for methods that require a timespan resolution
        /// </summary>
        public enum IntervalSize
        {
            Millisecond = 1,
            Second = 1000,
            Minute = 60000,
            QuarterHour = 900000,
            HalfHour = 1800000,
            Hour = 3600000,
            Day = 86400000
        }

        #endregion Enumerations

        #region Methods

        /// <summary>
        /// this interval occurs after the passed in interval 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool After(this Interval a, Interval b)
        {
            return b.Before(a);
        }

        /// <summary>
        /// this interval occurs before the passed in interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Before(this Interval a, Interval b)
        {
            return a.UtcEndTime < b.UtcStartTime;
        }

        /// <summary>
        /// this interval is started by the passed in interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool BeganBy(this Interval a, Interval b)
        {
            return b.Begins(a);
        }

        /// <summary>
        /// this interval starts the passed in interval, but ends before or at the same time
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Begins(this Interval a, Interval b)
        {
            return (a.UtcStartTime == b.UtcStartTime) && (a.UtcEndTime <= b.UtcEndTime);
        }

        /// <summary>
        /// the start of one interval is no more than one IntervalSize from the end of the other interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="intervalSize"></param>
        /// <returns></returns>
        public static bool CanMergeWith(this Interval a, Interval b, IntervalSize intervalSize)
        {
            return a.Overlaps(b) || a.OverlapedBy(b) || a.Meets(b, intervalSize);
        }

        /// <summary>
        /// the passed in interval is fully contained by this interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Contains(this Interval a, Interval b)
        {
            return (b.UtcStartTime > a.UtcStartTime) && (b.UtcEndTime < a.UtcEndTime);
        }

        /// <summary>
        /// this interval is fully contained by the passed in interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool During(this Interval a, Interval b)
        {
            return b.Contains(a);
        }

        /// <summary>
        /// the passed in interval ends this interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool EndedBy(this Interval a, Interval b)
        {
            return b.Ends(a);
        }

        /// <summary>
        /// this interval has the same end time as the passed in interval
        /// but this interval's start time is >= the start time of the passed in interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Ends(this Interval a, Interval b)
        {
            return (a.UtcEndTime == b.UtcEndTime) && (a.UtcStartTime >= b.UtcStartTime);
        }

        /// <summary>
        /// the passed in interval includes this interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IncludedBy(this Interval a, Interval b)
        {
            return b.Includes(a);
        }

        /// <summary>
        /// the start and end times of the passed in interval
        /// are the same or contained by this interval's start and end times
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Includes(this Interval a, Interval b)
        {
            return (b.UtcStartTime >= a.UtcStartTime) && (b.UtcEndTime <= a.UtcEndTime);
        }

        /// <summary>
        /// the common, overlapping portions of the interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Interval Intersect(this Interval a, Interval b)
        {
            if(a.Overlaps(b))
            {
                return new Interval(a.UtcStartTime.Max(b.UtcStartTime), a.UtcEndTime.Min(b.UtcEndTime));
            }
            throw new System.ArgumentException(string.Format("{0} cannot overlap with {1}", a.ToString(), b.ToString()));
        }

        /// <summary>
        /// true if the intervals have the same start and end times
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsEqualTo(this Interval a, Interval b)
        {
            return (a.UtcStartTime == b.UtcStartTime) && (a.UtcEndTime == b.UtcEndTime);
        }

        /// <summary>
        /// true if the start and end times of the intervals are within an IntervalSize of each other
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="intervalSize"></param>
        /// <returns></returns>
        public static bool Meets(this Interval a, Interval b, IntervalSize intervalSize)
        {
            return (b.UtcStartTime == a.UtcEndTime.RoundUp(System.TimeSpan.FromMilliseconds((int)intervalSize))
                || a.UtcStartTime == b.UtcEndTime.RoundUp(System.TimeSpan.FromMilliseconds((int)intervalSize)));
        }

        /// <summary>
        /// true if the passed in interval meets this interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="intervalSize"></param>
        /// <returns></returns>
        public static bool MetBy(this Interval a, Interval b, IntervalSize intervalSize)
        {
            return b.Meets(a, intervalSize);
        }

        /// <summary>
        /// creates a new interval where the 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="intervalSize"></param>
        /// <returns></returns>
        public static Interval Minus(this Interval a, Interval b, IntervalSize intervalSize)
        {
            if(a.CanMergeWith(b, intervalSize) && (a.UtcStartTime < b.UtcStartTime) && (a.UtcEndTime <= b.UtcEndTime))
            {
                if (a.UtcStartTime < b.UtcStartTime && a.UtcEndTime <= b.UtcEndTime)
                {
                    return new Interval(a.UtcStartTime, a.UtcEndTime.Min(b.UtcStartTime.AddMilliseconds(-(int)intervalSize)));
                }
                else if(a.CanMergeWith(b, intervalSize) && (a.UtcStartTime >= b.UtcStartTime) && (a.UtcEndTime > b.UtcEndTime))
                {
                    return new Interval(b.UtcEndTime.AddMilliseconds((int)intervalSize).Max(a.UtcStartTime), a.UtcEndTime);
                }
                throw new System.ArgumentException(string.Format("{0} cannot Minus with {1}", a.ToString(), b.ToString()));
            }
            throw new System.ArgumentException(string.Format("{0} cannot Minus with {1}", a.ToString(), b.ToString()));
        }

        public static bool OverlapedBy(this Interval a, Interval b)
        {
            return b.Overlaps(a);
        }

        /// <summary>
        /// true if the passed in interval starts inside this interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Overlaps(this Interval a, Interval b)
        {
            return (a.UtcStartTime <= b.UtcEndTime) && (b.UtcStartTime <= a.UtcEndTime);
        }

        /// <summary>
        /// creates a new interval with the min start time and the max end time of the two intervals
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="intervalSize"></param>
        /// <returns></returns>
        public static Interval Union(this Interval a, Interval b, IntervalSize intervalSize)
        {
            if(a.CanMergeWith(b, intervalSize))
            {
                return new Interval(a.UtcStartTime.Min(b.UtcStartTime), a.UtcEndTime.Max(b.UtcEndTime));
            }
            throw new System.ArgumentException(string.Format("{0} cannot be merged with {1}", a.ToString(), b.ToString()));
        }

        #endregion Methods
    }
}