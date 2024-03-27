using AInterpreter.lib.Core.Interpreter.Statements.Comparison;

namespace AInterpreter.lib.Core.Runtime.Memory.Variables
{
    public enum VariableType
        {
            STRING,
            INTEGER,
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
            this.variableType   = VariableType.INTEGER;
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
            if(this.variableType != VariableType.INTEGER)
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
                case VariableType.INTEGER:
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
            if(this.variableType != VariableType.INTEGER)
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
        public bool Compare(Variable variableToCompare, ComparisonOperator comparisonOperator, bool reversed = false)
        {
            return Compare(variableToCompare.GetIntValue(), comparisonOperator, reversed);
        }

        public bool Compare(int? valueToCompare, ComparisonOperator comparisonOperator, bool reversed = false)
        {
            if(reversed)
            {
                return CompareReversed(valueToCompare, comparisonOperator);
            }
            switch (comparisonOperator)
            {
                case ComparisonOperator.LessThan:
                {
                    return GetIntValue() < valueToCompare;
                }
                case ComparisonOperator.GreaterThan:
                {
                    return GetIntValue() > valueToCompare;
                }
                case ComparisonOperator.LessThanOrEqual:
                {
                    return GetIntValue() <= valueToCompare;
                }
                case ComparisonOperator.greaterThanOrEqual:
                {
                    return GetIntValue() >= valueToCompare;
                }
                case ComparisonOperator.Equal:
                {
                    return GetIntValue() == valueToCompare;
                }
                case ComparisonOperator.NotEqual:
                {
                    return GetIntValue() != valueToCompare;
                }
                default:
                {
                    throw new ArgumentException("Invalid comparison operator!");
                }
            }
        }
        public bool CompareReversed(int? valueToCompare, ComparisonOperator comparisonOperator)
        {
            switch (comparisonOperator)
            {
                case ComparisonOperator.LessThan:
                {
                    return valueToCompare < GetIntValue();
                }
                case ComparisonOperator.GreaterThan:
                {
                    return valueToCompare > GetIntValue();
                }
                case ComparisonOperator.LessThanOrEqual:
                {
                    return valueToCompare <= GetIntValue() ;
                }
                case ComparisonOperator.greaterThanOrEqual:
                {
                    return valueToCompare >= GetIntValue();
                }
                case ComparisonOperator.Equal:
                {
                    return valueToCompare == GetIntValue();
                }
                case ComparisonOperator.NotEqual:
                {
                    return valueToCompare != GetIntValue();
                }
                default:
                {
                    throw new ArgumentException("Invalid comparison operator!");
                }
            }
        }

    }
}