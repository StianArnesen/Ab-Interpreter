using System;

namespace AInterpreter.Exceptions
{
    public class EmptyInstructionSet :AException
    {
        // Default constructor

        // Constructor with a custom error message
        public EmptyInstructionSet(string message) : base(message) { }

        // Constructor with a custom error message and inner exception
        public EmptyInstructionSet(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
