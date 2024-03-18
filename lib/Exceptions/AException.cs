using System;

namespace AInterpreter.Exceptions
{
    public class AException : System.Exception
    {
        public override string Message { get; }
        public int LineNumber { get; private set; } = 0;
        public AException(string message, int lineNumber) 
        {
            this.LineNumber = lineNumber;
            this.Message = $"{message} |||| LINE NUMBER: {lineNumber}";
        }
        public AException(string message, System.Exception innerException) 
        {
            this.Message = message + " |||| INNER EXCEPTION: " + innerException.Message;
        }
    }
}

