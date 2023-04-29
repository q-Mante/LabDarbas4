using System;

namespace LabDarbas4_19.App_Class
{
    /// <summary>
    /// Custom exception class for when an object is empty.
    /// </summary>
    public class EmptyException : Exception
    {
        private string messageDetails = string.Empty;

        /// <summary>
        /// Timestamp of when the error occurred.
        /// </summary>
        public DateTime ErrorTimeStamp { get; set; }
        
        /// <summary>
        /// Cause of the error.
        /// </summary>
        public string CauseOfError { get; set; }

        /// <summary>
        /// Default constructor for EmptyException.
        /// </summary>
        public EmptyException()
            : base("Empty object!") { }

        /// <summary>
        /// Constructor that takes a message parameter.
        /// </summary>
        /// <param name="message">Error message.</param>
        public EmptyException(string message)
            :base(message)
        {
            messageDetails = message;
        }
        /// <summary>
        /// Constructor that takes a message and an inner exception parameter.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="ex2">Inner exception.</param>
        public EmptyException(string message, Exception ex2)
            : base(message, ex2)
        {
            messageDetails = message;
        }

        /// <summary>
        /// Constructor that takes a message, timestamp, and cause of error parameters.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="errorTimeStamp">Timestamp of when the error occurred.</param>
        /// <param name="causeOfError">Cause of the error.</param>
        public EmptyException(string message, DateTime errorTimeStamp, string causeOfError)
        {
            messageDetails = message;
            ErrorTimeStamp = errorTimeStamp;
            CauseOfError = causeOfError;
        }
    }
}