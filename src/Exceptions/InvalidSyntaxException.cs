namespace AInterpreter.lib.Exceptions
{
    public class InvalidSyntaxException : AbException
    {
        public InvalidSyntaxException(string message, int lineNumber) : base(message, lineNumber) { }

        public InvalidSyntaxException(string message, int lineNumber, Exception innerException) : base(message, lineNumber, innerException) { }
    }
}
