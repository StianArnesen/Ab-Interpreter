using System;

namespace AInterpreter.Exceptions
{
    public class InvalidSyntaxException : AException
    {
        // Default constructor

        // Constructor with a custom error message
        public InvalidSyntaxException(string message, int lineNumber) : base(message, lineNumber) { }

        // Constructor with a custom error message and inner exception
        public InvalidSyntaxException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
