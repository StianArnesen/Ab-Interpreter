using System;

namespace AInterpreter.Exceptions
{
    public class VariableNotFoundException : System.Exception
    {
        // Default constructor
        public VariableNotFoundException() { }

        // Constructor with a custom error message
        public VariableNotFoundException(string message) : base(message) { }

        // Constructor with a custom error message and inner exception
        public VariableNotFoundException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
