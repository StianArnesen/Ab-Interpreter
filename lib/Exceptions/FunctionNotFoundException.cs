using System;

namespace AInterpreter.Exceptions
{
    public class FunctionNotFoundException : System.Exception
    {
        public FunctionNotFoundException()
        {
        }

        //Override the default constructor with a custom error message 
        public FunctionNotFoundException(string message) : base(message)
        {
            
        }

        /*public FunctionNotFoundException(string message) : base($"Function {message}() was not found in the function set.");
        {
        }*/

        public FunctionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}