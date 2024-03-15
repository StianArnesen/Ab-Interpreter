using System;

namespace AInterpreter.Exceptions
{
    public class AException : System.Exception
    {
        public override string Message { get; }
        public AException(string message) 
        {
            this.Message = message;
        }
        public AException(string message, System.Exception innerException) 
        {
            this.Message = message + " |||| INNER EXCEPTION: " + innerException.Message;
        }
    }
}

