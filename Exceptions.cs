using System;

namespace Strange1.Utility.DateTimeExtensions
{
    public class InvalidTimePeriodException : Exception
    {
        public InvalidTimePeriodException()
            : base() { }

        public InvalidTimePeriodException(string message)
            : base(message) { }

        public InvalidTimePeriodException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public InvalidTimePeriodException(string message, Exception innerException)
            : base(message, innerException) { }

        public InvalidTimePeriodException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}