namespace AInterpreter.Core.Runtime
{
    public enum VariableType
        {
            STRING,
            INT,
            ARRAY,
            UNSET
        }

    public class Variable
    {
        public string VariableName {get;}
        public string StringValue {get; private set;} = "";
        public int IntValue {get;private set;} = 0;
        private VariableType variableType {get;set;}

        public Variable[]? variables {get;set;}

        public static int? operator +(Variable a, Variable b)
        {
            int? sum = a.IntValue + b.IntValue;
            return sum;
        }
        public static int? operator -(Variable a, Variable b)
        {
            int? sum = a.IntValue - b.IntValue;
            return sum;
        }
        
        public Variable(string variableName, int intValue)
        {
            this.variableType   = VariableType.INT;
            this.VariableName   = variableName;
            this.IntValue       = intValue;
        }

        public Variable(string variableName, string stringValue)
        {
            this.variableType   = VariableType.STRING;
            this.VariableName   = variableName;
            this.StringValue    = stringValue;
        }
        
        public int GetIntValue()
        {
            if(this.variableType != VariableType.INT)
            {
                throw new InvalidCastException($"Variable '{this.VariableName}' is not an integer!");
            }
            return this.IntValue;
        }
        public string GetStringValue()
        {
            if(this.variableType != VariableType.STRING)
            {
                throw new InvalidCastException($"Variable '{this.VariableName}' is not a string!");
            }
            return this.StringValue;
        }

        public string GetValuesToString()
        {
            switch (this.variableType)
            {
                case VariableType.STRING:
                {
                    return this.StringValue;
                }
                case VariableType.INT:
                {
                    return this.IntValue.ToString();
                }
                default:
                {
                    return "_UNSET_VARIABLE_";
                }
            }
        }

        public void SetValue(int value)
        {
            if(this.variableType != VariableType.INT)
            {
                throw new InvalidCastException($"Variable '{this.VariableName}' is not an integer!");
            }
            this.IntValue = value;
        }
        public void SetValue(string value)
        {
            if(this.variableType != VariableType.STRING)
            {
                throw new InvalidCastException($"Variable '{this.VariableName}' is not a string!");
            }
            this.StringValue = value;
        }

    }
}