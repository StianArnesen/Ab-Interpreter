using System;

namespace AInterpreter.Exceptions
{
    public class InvalidSyntaxException : System.Exception
    {
        // Default constructor
        public InvalidSyntaxException() { }

        // Constructor with a custom error message
        public InvalidSyntaxException(string message) : base(message) { }

        // Constructor with a custom error message and inner exception
        public InvalidSyntaxException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
