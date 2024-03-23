using System.Text.RegularExpressions;
using AInterpreter.Core.Logger;
using AInterpreter.Signatures;

namespace AInterpreter.Core.Interpreter.Statements.Comparison
{
    /* ! All methods in this class was created with Chat GPT ! */
    public static class ComparisonOperatorExtension
    {
        public static string GetStringRepresentation(this ComparisonOperator? comparisonOperator)
        {
            var type = typeof(ComparisonOperator);
            var memInfo = type.GetMember(comparisonOperator.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(StringValueAttribute), false);
            return attributes.Length > 0 ? ((StringValueAttribute)attributes[0]).Value : comparisonOperator.ToString();
        }

        public static ComparisonOperator GetOperatorFromString(string value)
        {
            var type = typeof(ComparisonOperator);
            foreach (var name in Enum.GetNames(type))
            {
                var memInfo = type.GetMember(name);
                var attributes = memInfo[0].GetCustomAttributes(typeof(StringValueAttribute), false);
                if (attributes.Length > 0 && ((StringValueAttribute)attributes[0]).Value == value)
                    return (ComparisonOperator)Enum.Parse(type, name);
            }
            throw new ArgumentException("No matching operator found for the given string value.", nameof(value));
        }
        public static ComparisonOperator ExtractSingleComparisonOperator(string line)
        {
            Match match = Regex.Match(line, ComparisonSignatures.ALL_COMPARISON_SIGNATURES);

            if (match.Success)
            {
                DebugLog.Log("Comparison operator found in the condition: " + match.Value, "Core.Runtime.StatementInterpreter");
                return GetOperatorFromString(match.Value);
            }
            else
            {
                throw new ArgumentException("Comparison operator not found in the condition.");
            }
        }
    }
    
}
