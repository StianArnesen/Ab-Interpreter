using System;

namespace AInterpreter.Exceptions
{
    public class EndOfQueueException : System.Exception
    {
        // Default constructor
        public EndOfQueueException() { }

        // Constructor with a custom error message
        public EndOfQueueException(string message) : base(message) { }

        // Constructor with a custom error message and inner exception
        public EndOfQueueException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
