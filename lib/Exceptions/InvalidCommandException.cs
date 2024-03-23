namespace AInterpreter.Exceptions
{
    public class InvalidInstruction : AbException
    {
        public InvalidInstruction(string message, int lineNumber) : base(message,lineNumber) { }

        public InvalidInstruction(string message, int lineNumber, Exception innerException) : base(message, lineNumber, innerException) { }
    }
}
