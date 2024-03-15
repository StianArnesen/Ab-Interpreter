using System;

namespace AInterpreter.Exceptions
{
    public class InvalidInstruction : System.Exception
    {
        // Default constructor
        public InvalidInstruction() { }

        // Constructor with a custom error message
        public InvalidInstruction(string message) : base(message) { }

        // Constructor with a custom error message and inner exception
        public InvalidInstruction(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
