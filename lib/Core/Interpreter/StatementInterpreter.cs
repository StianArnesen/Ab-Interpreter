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

            line = line.Remove(0, indexOfCloseParentheses);
            
            string functionTrueName  = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_TRUE, GlobalSignatures.OPEN_PARENTHESIS);
            
            int? leftHandValueInt = null;
            int? rightHandValueInt = null;
            
            try{ leftHandValueInt = int.Parse(leftHandValue); }catch(FormatException e){} //   If left hand value is a variable, it will be handled later.
            

            try{ rightHandValueInt = int.Parse(rightHandValue); }catch(FormatException e){} // If right hand value is a variable, it will be handled later.
            
            
            if(leftHandValueInt == null && rightHandValueInt != null)
            {
                string leftHandVariableName = leftHandValue.Trim();
                int indexOfStatementFalseSignature = line.IndexOf(StatementSignatures.IF_STATEMENT_FALSE);
                
                line = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_FALSE, GlobalSignatures.END_OF_LINE);

                string functionFalseName = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_FALSE, GlobalSignatures.OPEN_PARENTHESIS);
                

                programMemory.AddInstructionToCurrentFunction(new Instruction(programMemory, () => 
                {
                    int variableValue = programMemory.VariableController.GetVariable(leftHandVariableName).GetIntValue();
                    if(variableValue < rightHandValueInt)
                    {
                        //programMemory.RemoveCurrentFunction();
                        programMemory.AddFunctionToExecutionStack(functionTrueName);
                    }
                    else
                    {
                        //programMemory.RemoveCurrentFunction();
                        programMemory.AddFunctionToExecutionStack(functionFalseName);
                    }
                }));
            }
            else if(leftHandValueInt == null && rightHandValueInt == null)
            {
                string leftHandVariableName = leftHandValue.Trim();
                string rightHandVariableName = rightHandValue.Trim();
                
                line = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_FALSE, GlobalSignatures.END_OF_LINE);

                string functionFalseName = new StringHelper(line).FromFirstToNextIndentifier(StatementSignatures.IF_STATEMENT_FALSE, GlobalSignatures.OPEN_PARENTHESIS);
                

                programMemory.AddInstructionToCurrentFunction(new Instruction(programMemory, () => 
                {
                    int leftValue = programMemory.VariableController.GetVariable(leftHandVariableName).GetIntValue();
                    int rightValue = programMemory.VariableController.GetVariable(rightHandVariableName).GetIntValue();
                    if(leftValue < rightValue)
                    {
                        //programMemory.RemoveCurrentFunction();
                        programMemory.AddFunctionToExecutionStack(functionTrueName);
                    }
                    else
                    {
                        //programMemory.RemoveCurrentFunction();
                        programMemory.AddFunctionToExecutionStack(functionFalseName);
                    }
                }));
            }
        }
    }
}