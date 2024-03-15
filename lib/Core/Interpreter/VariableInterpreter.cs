using AInterpreter.Core.Runtime;
using AInterpreter.Core.Logger;
using AInterpreter.Core.Signatures;
using AInterpreter.Exceptions;
using System.Collections.Generic;

namespace AInterpreter.Interpreter
{
    public class VariableInterpreter
    {
        public static Instruction? GetInstruction(ProgramMemory program, string line)
        {
            if(line.StartsWith(VariableSignatures.INTEGER_INITIALIZE))
            {
                return GetInstructionSetForIntegerInitializer(program, line);
            }
            else if(line.StartsWith(VariableSignatures.STRING_INITIALIZE))
            {
                return GetInstructionSetForStringInitializer(program, line);
            }
            else if(line.Contains(GlobalSignatures.OPERATOR_EQUALS))
            {
                return GetInstructionSetForVariableModifier(program, line);
            }
            DebugLog.Log($"Could not find any suitable instructions for this line: {line} ! ", "VariableInterpreter");
            return null;
        }

        public static void interpretVariableSignature(ProgramMemory program, string line)
        {
            Instruction? instruction = GetInstruction(program,line);
            program.AddInstructionToCurrentFunction(instruction);
        }

        private static Instruction GetInstructionSetForVariableModifier(ProgramMemory program, string line)
        {
            int varNameLength   = line.IndexOf(GlobalSignatures.OPERATOR_EQUALS) - 1;
            string variableName = line.Trim().Substring(0, varNameLength);
            int variableValue   = getVariableIntegerValue(line);
            
            return new Instruction(program, () =>{program.VariableController.SetVariable(variableName, variableValue);});            
        }

        private static Instruction GetInstructionSetForIntegerInitializer(ProgramMemory program, string line)
        {            
            string variableName    = GetVariableName(line);

            int variableValue = getVariableIntegerValue(line);
            
            return new Instruction(program, () =>{program.VariableController.AddVariable(variableName, variableValue);});
        }

        private static int getVariableIntegerValue(string line)
        {
            string noSpace = new StringHelper(line).RemoveWhiteSpace();
            int variableValueIndexStart = noSpace.IndexOf(GlobalSignatures.OPERATOR_EQUALS) + 1;
            int variableValueLength     = noSpace.IndexOf(GlobalSignatures.END_OF_LINE) - variableValueIndexStart;
            string stringValue          = noSpace.Substring(variableValueIndexStart, variableValueLength);

            int integerValue   = int.Parse(stringValue);

            return integerValue;
        }

        private static string getVariableStringValue(string line)
        {
            int variableValueIndexStart = line.Trim().IndexOf(GlobalSignatures.OPERATOR_EQUALS) + 3;
            int variableValueLength     = line.Trim().IndexOf(GlobalSignatures.END_OF_LINE) - variableValueIndexStart -1;

            string stringValue   = line.Trim().Substring(variableValueIndexStart, variableValueLength);

            return stringValue;
        }

        public static Instruction GetInstructionSetForStringInitializer(ProgramMemory program, string line)
        {
            string variableName = GetVariableName(line);
            
            string variableValue = getVariableStringValue(line);
            
            return new Instruction(program, () =>{program.VariableController.AddVariable(variableName, variableValue);});
        }
        
        public static string GetVariableName(string line, string? startOfNameChar = VariableSignatures.END_OF_VARIABLE_TYPE_CHAR, string endOfNameChar = GlobalSignatures.OPERATOR_EQUALS)
        {
            string noSpace = new StringHelper(line).RemoveWhiteSpace();
            
            int variableNameIndexStart = startOfNameChar != null ? noSpace.IndexOf(startOfNameChar) + 1 : 0;
            int variableNameLength     = noSpace.IndexOf(endOfNameChar) - variableNameIndexStart;
            
            if(variableNameIndexStart < 0)
            {
                throw new InvalidSyntaxException("Excpected ':' after type definition!");
            }

            string variableName    = noSpace.Substring(variableNameIndexStart, variableNameLength);

            return variableName;
        }

    }
}