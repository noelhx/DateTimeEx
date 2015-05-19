using System;

namespace Strange1.Utility.DateTimeExtensions
{
    /// <summary>
    /// exception thrown when trying to create an interval with a start time greater than the end time
    /// </summary>
    public class InvalidIntervalException : Exception
    {
        #region Constructors

        public InvalidIntervalException()
            : base()
        {
        }

        public InvalidIntervalException(string message)
            : base(message)
        {
        }

        public InvalidIntervalException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public InvalidIntervalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidIntervalException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        #endregion Constructors
    }
}