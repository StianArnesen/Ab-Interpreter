using AInterpreter.Core.Signatures;
using AInterpreter.Interpreter;

namespace AInterpreter.Core.Runtime.Commands
{
    public static class StatementInterpreter
    {
        public static void HandleIfStatementSignature(ProgramMemory programMemory, string line)
        {
            interpretStatementWithOperator(programMemory, line, "");
        }
        private static void interpretStatementWithOperator(ProgramMemory programMemory, string line, string operatorType)
        {
            string leftHandValue   = line.Split(StatementSignatures.LESS_THAN)[0];
            string rightHandValue  = line.Split(StatementSignatures.LESS_THAN)[1];

            int indexOfOpenParentheses  =  leftHandValue.IndexOf(GlobalSignatures.OPEN_PARENTHESIS);
            int indexOfCloseParentheses =  line.IndexOf(GlobalSignatures.CLOSE_PARENTHESIS);
            
            int leftLength = leftHandValue.Length;
            leftHandValue  = leftHandValue.Substring(indexOfOpenParentheses + 1, leftLength - indexOfOpenParentheses - 1);
            rightHandValue = new StringHelper(rightHandValue).GetSubstringFromIndexToSequence(0, GlobalSignatures.CLOSE_PARENTHESIS);

            Console.WriteLine("leftHandValue: " + leftHandValue);
            Console.WriteLine("rightHandValue: " + rightHandValue);

            line = line.Remove(0, indexOfCloseParentheses);
            
            int? leftHandValueInt = null;
            int? rightHandValueInt = null;
            
            try
            {
                leftHandValueInt = int.Parse(leftHandValue);
                // leftHandValueInt is a number
            }
            catch(Exception e)
            {
                // leftHandValueInt is not a number
            }

            try
            {
                rightHandValueInt = int.Parse(rightHandValue);
                // rightHandValueInt is a number
            }
            catch(Exception e)
            {
                // rightHandValueInt is not a number
            }
            if(leftHandValueInt == null && rightHandValueInt != null)
            {
                string leftHandVariableName = leftHandValue.Trim();
                string functionTrueName  = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_TRUE, GlobalSignatures.OPEN_PARENTHESIS);
                int indexOfStatementFalseSignature = line.IndexOf(StatementSignatures.IF_STATEMENT_FALSE);
                
                line = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_FALSE, GlobalSignatures.END_OF_LINE);

                string functionFalseName = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_FALSE, GlobalSignatures.OPEN_PARENTHESIS);
                

                programMemory.AddInstructionToCurrentFunction(new Instruction(programMemory, () => {
                    int variableValue = int.Parse(programMemory.VariableController.GetVariable(leftHandVariableName).Value.ToString());
                    if(variableValue < rightHandValueInt)
                    {
                        programMemory.AddFunctionToExecutionStack(functionTrueName);
                    }
                    else
                    {
                        programMemory.AddFunctionToExecutionStack(functionFalseName);
                    }
                }));
            }

            Console.WriteLine("leftHandValueInt: " + leftHandValueInt);
            Console.WriteLine("rightHandValueInt: " + rightHandValueInt);
        }
    }
}