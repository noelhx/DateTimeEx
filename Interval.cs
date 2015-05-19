using System;
using System.Globalization;

namespace Strange1.Utility.DateTimeExtensions
{
    /// <summary>
    /// simple interval class to support interval algebra as per Allen's Interval Algebra
    /// see https://www.ics.uci.edu/~alspaugh/cls/shr/allen.html
    /// </summary>
    public class Interval : IComparable
    {
        #region Variables

        #region Member Variables

        private DateTime _startTime = DateTime.MinValue;
        private DateTime _endTime = DateTime.MaxValue;

        #endregion Member Variables

        #endregion Variables

        #region Constructors

        /// <summary>
        /// instantiates a new Interval
        /// </summary>
        /// <param name="starttime">the begining of the interval</param>
        /// <param name="endtime">the end of the interval</param>
        public Interval(DateTime starttime, DateTime endtime)
        {
            if (starttime < endtime)
            {
                this._startTime = starttime;
                this._endTime = endtime;
            }
            else
            {
                throw new InvalidIntervalException("StartTime [{0}] must be less than EndTime [{1}]", starttime.ToString("o"), endtime.ToString("o"));
            }
        }

        /// <summary>
        /// instantiates a new interval based upon a start time and a duration
        /// </summary>
        /// <param name="starttime">the begining of the interval</param>
        /// <param name="duration">the duration of the interval to create</param>
        public Interval(DateTime starttime, TimeSpan duration)
        {
            this._startTime = starttime;
            this._endTime = starttime + duration;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Total duration of the interval
        /// </summary>
        public TimeSpan Duration
        {
            get { return this._endTime - this._startTime; }
        }

        /// <summary>
        /// the end of the interval
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                if (value > _startTime)
                {
                    _endTime = value;
                }
                else
                {
                    throw new InvalidIntervalException("EndTime {0} must be greater than StartTime {1}", value, _startTime.ToString("o"));
                }
            }
        }

        /// <summary>
        /// the begining of the interval
        /// </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                if (value < _endTime)
                {
                    _startTime = value;
                }
                else
                {
                    throw new InvalidIntervalException("StartTime {0} must be less than EndTime {1}", value, _endTime.ToString("o"));
                }
            }
        }

        /// <summary>
        /// the UTC version of the end time
        /// </summary>
        public DateTime UtcEndTime
        {
            get
            {
                return _endTime.ToUniversalTime();
            }
        }

        /// <summary>
        /// the UTC version of the start time
        /// </summary>
        public DateTime UtcStartTime
        {
            get
            {
                return _startTime.ToUniversalTime();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// combines two intervals together
        /// </summary>
        /// <param name="lhs">the left-hand item</param>
        /// <param name="rhs">the right-hand item</param>
        /// <returns></returns>
        public static Interval operator +(Interval lhs, Interval rhs)
        {
            return new Interval(lhs.UtcStartTime.Min(rhs.UtcStartTime), lhs.UtcEndTime.Max(rhs.UtcEndTime));
        }

        /// <summary>
        /// provides the standard CompareTo interface
        /// </summary>
        /// <param name="targetObj"></param>
        /// <returns></returns>
        int IComparable.CompareTo(object targetObj)
        {
            if (targetObj == null)
            {
                return -1;
            }

            try
            {
                Interval target = targetObj as Interval;

                if (this.IsEqualTo(target))
                {
                    return 0;
                }
                else if (this.Ends(target))
                {
                    return 1;
                }
                else if (this.Begins(target))
                {
                    return -1;
                }
                else if (target.Begins(this))
                {
                    return 1;
                }
                return -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// tests for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Interval target = obj as Interval;
            if ((object)target == null)
            {
                return false;
            }
            return this.IsEqualTo(target);
        }

        /// <summary>
        /// tests for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Interval other)
        {
            return other == null ? false : this.IsEqualTo(other);
        }

        /// <summary>
        /// gets a unique hash code for this interval
        /// </summary>
        /// <returns>the integer hash code</returns>
        public override long GetHashCode()
        {
            return (long)(this.StartTime.Hash() ^ this.EndTime.Hash());
        }

        /// <summary>
        /// formatted ToString method
        /// </summary>
        /// <returns>S[2015-05-05T13:05:00.00Z] -> S[2015-05-05T13:06:00.00Z] : D[00:01]</returns>
        public override string ToString()
        {
            return String.Format("S[{0}] -> E[{1}] : D[{2}]", this.StartTime.ToString("o"), this.EndTime.ToString("o"), this.Duration);
        }

        #endregion Methods
    }
}