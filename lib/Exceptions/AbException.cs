namespace AInterpreter.lib.Exceptions
{
    public class AbException : Exception
    {
        public string Message  { get; set; }
        private int LineNumber { get; set; } = 0;

        public AbException(string message) 
        {
            this.Message = message;
        }
        public AbException(string message, object source) 
        {
            this.Message = $"{message} |||| Source: {source}";
        }
        public AbException(string message, int lineNumber) 
        {
            this.LineNumber = lineNumber;
            this.Message = $"{message} |||| @LINE NUMBER: {lineNumber}";
        }
        public AbException(string message, int lineNumber, Exception innerException) 
        {
            this.Message    = message + " |||| INNER EXCEPTION: " + innerException.Message;
            this.LineNumber = lineNumber;
        }
    }
}

