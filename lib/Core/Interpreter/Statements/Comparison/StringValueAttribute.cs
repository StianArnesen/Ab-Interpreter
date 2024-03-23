namespace AInterpreter.Core.Interpreter.Statements.Comparison
{
    public class StringValueAttribute : Attribute
    {
        public string Value { get; private set; }

        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }
    
}
