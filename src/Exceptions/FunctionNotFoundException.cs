namespace AInterpreter.lib.Exceptions
{
    public class FunctionNotFoundException : AbException
    {
        public FunctionNotFoundException(string funcName) : base(funcName) 
        { 
            Message = $"Function not found: '{funcName}()'";
        }
        public FunctionNotFoundException(string message, int lineNumber) : base(message, lineNumber) { }

        public FunctionNotFoundException(string message, int lineNumber, Exception innerException) : base(message, lineNumber, innerException) { }
    }
}