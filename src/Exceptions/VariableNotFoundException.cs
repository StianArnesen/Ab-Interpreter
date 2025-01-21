namespace AInterpreter.lib.Exceptions
{
    public class VariableNotFoundException : AbException
    {
        public VariableNotFoundException(string message) : base(message) { }
        public VariableNotFoundException(string message, object source) : base(message, source) { }
        public VariableNotFoundException(string message, int lineNumber) : base(message, lineNumber) { }

        public VariableNotFoundException(string message, int lineNumber , Exception innerException) : base(message, lineNumber, innerException) { }
    }
}
