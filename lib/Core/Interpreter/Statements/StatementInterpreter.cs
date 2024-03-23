using System.Text.RegularExpressions;
using AInterpreter.Core.Interpreter.Statements;
using AInterpreter.Core.Interpreter.Statements.Comparison;
using AInterpreter.Core.Logger;
using AInterpreter.Core.Signatures;
using AInterpreter.Interpreter;
using AInterpreter.Signatures;

namespace AInterpreter.Core.Runtime.Commands
{
    public static class StatementInterpreter
    {
        public static void HandleIfStatementSignature(ProgramMemory programMemory, string line)
        {
            ComparisonOperator parsedOperator = ComparisonOperatorExtension.ExtractSingleComparisonOperator(line);
            
            interpretStatementWithOperator(programMemory, line, parsedOperator);
        }

        private static void interpretStatementWithOperator(ProgramMemory programMemory, string line, ComparisonOperator @operator)
        {
            string operatorType = ComparisonOperatorExtension.GetStringRepresentation(@operator);

            string leftHandValue   = line.Split(operatorType)[0];
            string rightHandValue  = line.Split(operatorType)[1];

            int indexOfOpenParentheses  =  leftHandValue.IndexOf(GlobalSignatures.OPEN_PARENTHESIS);
            int indexOfCloseParentheses =  line.IndexOf(GlobalSignatures.CLOSE_PARENTHESIS);
            
            int leftLength = leftHandValue.Length;
            
            leftHandValue  = leftHandValue.Substring(indexOfOpenParentheses + 1, leftLength - indexOfOpenParentheses - 1);
            rightHandValue = new StringHelper(rightHandValue).GetSubstringFromIndexToSequence(0, GlobalSignatures.CLOSE_PARENTHESIS);

            line = line.Remove(0, indexOfCloseParentheses);
            
            string functionTrueName = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_TRUE, GlobalSignatures.OPEN_PARENTHESIS);
            
            line = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_FALSE, GlobalSignatures.END_OF_LINE);

            string functionFalseName = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_FALSE, GlobalSignatures.OPEN_PARENTHESIS);

            
            int? leftHandValueInt = null;
            int? rightHandValueInt = null;
            
            try{ leftHandValueInt = int.Parse(leftHandValue); }catch(FormatException e){}   // If left hand value is a variable, it will be handled later.
            try{ rightHandValueInt = int.Parse(rightHandValue); }catch(FormatException e){} // If right hand value is a variable, it will be handled later.
            
            
            if(leftHandValueInt == null && rightHandValueInt != null)
            {
                string leftHandVariableName = leftHandValue.Trim();
                
                programMemory.AddInstructionToCurrentFunction(ComparisonInterpreter.GetComparisonInstruction(programMemory, leftHandVariableName, rightHandValueInt, functionTrueName, functionFalseName, @operator, false) );
            }
            else if(leftHandValueInt != null && rightHandValueInt == null)
            {
                string rightHandVariableName = rightHandValue.Trim();
                
                programMemory.AddInstructionToCurrentFunction(ComparisonInterpreter.GetComparisonInstruction(programMemory, rightHandVariableName, leftHandValueInt, functionTrueName, functionFalseName, @operator, true) );
            }
            else if(leftHandValueInt == null && rightHandValueInt == null)
            {
                string leftHandVariableName = leftHandValue.Trim();
                string rightHandVariableName = rightHandValue.Trim();

                programMemory.AddInstructionToCurrentFunction(ComparisonInterpreter.GetComparisonInstruction(programMemory, leftHandVariableName, rightHandVariableName, functionTrueName, functionFalseName, @operator, false) );
            }
        }
        
    }
}