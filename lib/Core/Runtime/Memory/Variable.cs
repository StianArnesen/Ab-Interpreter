namespace AInterpreter.Core.Runtime
{
    public class Variable
    {
        public string VariableName {get;}
        public string? StringValue {get;set;}
        public int? IntValue {get;set;}
        public object Value {get;set;}

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
        
        public Variable(string variableName, object value)
        {
            this.VariableName   = variableName;            
            this.Value          = value;
            this.StringValue    = null;
            this.variables      = null;
        }
        public Variable(string variableName)
        {
            this.VariableName   = variableName;            
            this.Value          = "";
            this.StringValue    = null;
            this.variables      = null;
        }

    }
}