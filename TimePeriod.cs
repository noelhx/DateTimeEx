using System;

namespace Strange1.Utility.DateTimeExtensions
{
    public class TimePeriod : IComparable
    {
        public TimePeriod(DateTime starttime, DateTime endtime)
        {
            if (starttime < endtime)
            {
                this._startTime = starttime;
                this._endTime = endtime;
            }
            else
            {
                throw new InvalidTimePeriodException("StartTime {0} must be less than EndTime {1}", starttime.ToString("o"), endtime.ToString("o"));
            }
        }

        public TimePeriod(DateTime starttime, TimeSpan duration)
        {
            this._startTime = starttime;
            this._endTime = starttime + duration;
        }

        private DateTime _startTime = DateTime.MinValue;
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
                    throw new InvalidTimePeriodException("StartTime {0} must be less than EndTime {1}", value, _endTime.ToString("o"));
                }
            }

        }

        private DateTime _endTime = DateTime.MaxValue;
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
                    throw new InvalidTimePeriodException("EndTime {0} must be greater than StartTime {1}", value, _startTime.ToString("o"));
                }
            }
        }

        public DateTime UtcStartTime
        {
            get
            {
                return _startTime.ToUniversalTime();
            }
        }

        public DateTime UtcEndTime
        {
            get
            {
                return _endTime.ToUniversalTime();
            }
        }

        public TimeSpan Duration
        {
            get { return this._endTime - this._startTime; }
        }

        static public TimePeriod operator +(TimePeriod a, TimePeriod b)
        {
            if (a.Overlaps(b))
            {
                DateTime start = a.StartTime <= b.StartTime ? a.StartTime : b.StartTime;
                DateTime end = b.EndTime >= a.EndTime ? b.EndTime : a.EndTime;
                return new TimePeriod(start, end);
            }
            return null;
        }

        public override string ToString()
        {
            return String.Format("{0} -> {1} : {2}", this.StartTime, this.EndTime, this.Duration);
        }

        int IComparable.CompareTo(object targetObj)
        {

            if(targetObj == null)
            {
                return -1;
            }

            try
            {
                TimePeriod target = targetObj as TimePeriod;


                if (this.IsEqualTo(target))
                {
                    return 0;
                }
                else if (this.Finishes(target))
                {
                    return 1;
                }
                else if (this.Starts(target))
                {
                    return -1;
                }
                else if (target.Starts(this))
                {
                    return 1;
                }
                return -1;
            }
            catch(Exception)
            {
                return -1;
            }
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            TimePeriod target = obj as TimePeriod;
            if ((object)target == null)
            {
                return false;
            }
            return this.IsEqualTo(target);
        }

        public bool Equals(TimePeriod other)
        {
            return other == null ? false : this.IsEqualTo(other);
        }

        public override int GetHashCode()
        {
            return (int)(this.StartTime.Ticks ^ this.EndTime.Ticks);
        }
    }
}