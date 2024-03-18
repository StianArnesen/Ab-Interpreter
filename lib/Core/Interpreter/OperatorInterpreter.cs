using AInterpreter.Core.Signatures;
using AInterpreter.Core.Runtime;
using AInterpreter.Core.Logger;
using AInterpreter.Core.Runtime.Commands;

namespace AInterpreter.Interpreter
{
    static class OperatorInterpreter
    {
        public static void InterpretOperatorLine(ProgramMemory programMemory, string line)
        {
            string operatorType = GetOperatorType(line);
            string leftHandVariableName  = VariableInterpreter.GetVariableName(programMemory, line, null, operatorType);            
            string rightHandVariableName = new StringHelper(line).GetSubstringBetweenIndentifiers(operatorType, GlobalSignatures.END_OF_LINE);            
            

            if(operatorType == OperatorSignatures.PLUS_EQUALS)
            {
                Instruction instruction = new Instruction(programMemory, MathOperations.Add(programMemory, leftHandVariableName, rightHandVariableName));
                programMemory.AddInstructionToCurrentFunction(instruction);
                DebugLog.Log($"Adding instruction to add variable: '{rightHandVariableName}' to variable: '{leftHandVariableName}'" , "Static class: AInterpreter.Interpreter.OperatorInterpreter");            
                return;
            }
            
        }

        // Function removes all characters except for OperatorSgnatures.OPERATOR_CHARACTERS (e.g. +, -, *, /, +=, -=, *=, /=, etc.)
        private static string GetOperatorType(string line)
        {
            string operatorName = new StringHelper(line).RemoveAllExcept(OperatorSignatures.OPERATOR_CHARACTERS);
            return operatorName;
        }
    }

}