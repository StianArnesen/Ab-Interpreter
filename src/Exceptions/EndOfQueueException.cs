namespace AInterpreter.lib.Exceptions
{
    public class EndOfQueueException : AbException
    {
        public EndOfQueueException(string message, object source) : base(message, source) { }
        public EndOfQueueException(string message, int lineNumber) : base(message, lineNumber) { }

        public EndOfQueueException(string message, int lineNumber, Exception innerException) : base(message, lineNumber, innerException) { }
    }
}
