namespace AInterpreter.Exceptions
{
    public class EmptyInstructionSet :AbException
    {
        public EmptyInstructionSet(string message, int lineNumber) : base(message, lineNumber) { }

        public EmptyInstructionSet(string message, int lineNumber, Exception innerException) : base(message, lineNumber, innerException) { }
    }
}
