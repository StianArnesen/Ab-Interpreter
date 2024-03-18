using AInterpreter.Core;
using AInterpreter.Core.Logger;
using AInterpreter.Core.Runtime;
using AInterpreter.Core.Runtime.Commands;
using AInterpreter.Core.Signatures;

namespace AInterpreter.Interpreter
{
    class FunctionInterpreter
    {  
        /*            
            This method is executed once when the program starts. It iterates through all lines and interprets all function definitions.
            So that the program knows which user-defined functions are available when execution begins.
        */
        public static void InterpretAllFunctionDefinitionSignatures(string[] lines, ProgramMemory programMemory)
        {
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();   
                if(line.StartsWith(FunctionSignatures.FUNCTION_DEFINITION_SIGNATURE))
                {
                    InterpretDefinitionSignature(programMemory, trimmedLine);
                }
            }
            
        }
        /*
            Interpret an execution signature and add the function to the ProgramMemory.
        */
        private static void InterpretDefinitionSignature(ProgramMemory programMemory, string line)
        {
            Function function = GetFunctionToCreate(line);
            AddParametersToFunctionDefinition(programMemory, line, function);
            programMemory.AddFunction(function);
        }

        public static void InterpretExecutionSignature(ProgramMemory programMemory, string line)
        {
            string functionName = GetFunctionName(line, FunctionSignatures.FUNCTION_EXECUTION_SIGNATURE);
            
            //programMemory.AddInstructionsToCurrentFunction(getInstructionsForFunctionExecutionParams(programMemory, line, programMemory.GetFunctionByName(functionName)));
            
            programMemory.AddInstructionToCurrentFunction(new Instruction(programMemory, () => {
                programMemory.addInstructionsFromFunctionToExecutionStack(functionName);
            }));
        }
        
        public static void InterpretReturnSignature(ProgramMemory programMemory, string line)
        {
           // TODO: Implement return signature.
           throw new NotImplementedException("Return signature not implemented yet and should not be needed.");
        }

        private static Function GetFunctionToCreate(string line)
        {
            string functionName = GetFunctionName(line, FunctionSignatures.FUNCTION_DEFINITION_SIGNATURE);
            
            return new Function(functionName);
        }

        private static void AddParametersToFunctionDefinition(ProgramMemory programMemory, string line, Function function)
        {
            string[] parametersString = line.Split("(")[1].Split(")")[0].Split(",");
            
            foreach (string parameterName in parametersString)
            {
                if(parameterName.Trim().Equals(""))
                {
                    continue;
                }
                DebugLog.Log($"Adding parameter: {parameterName} to {function.Name}()", DebugLog.LogType.INFO);
                programMemory.VariableController.AddVariable(parameterName, "str_null");
            }
        }
        
        /*private static List<Instruction> getInstructionsForFunctionExecutionParams(ProgramMemory programMemory, string line, Function function)
        {
            List<Instruction> instructions = new List<Instruction>(); // The instructions to return.

            string[] parametersString = line.Split("(")[1].Split(")")[0].Split(",");
            
            int parameterIndex = 0;

            foreach (string parameterName in parametersString)
            {
                if(parameterName.Trim().Equals(""))
                {
                    continue;
                }
                if(parameterName.Contains('"'))
                {
                    string value = new StringHelper(parameterName).GetSubstringBetweenChars('"', '"');
                    instructions.Add(getInstructionToSetParameterToValue(programMemory, parameterName, value, function));
                }
                
                DebugLog.Log($"Adding parameter: {parameterName} to {function.Name}()", DebugLog.LogType.INFO);
                instructions.Add(getInstructionToSetParameterToVariable(programMemory, parameterIndex, function, parameterName));
                
                parameterIndex++;
            }

            return instructions;
        }

        private static Instruction getInstructionToSetParameterToVariable(ProgramMemory program, int parameterIndex, Function function, string parameterVariableName){
            return new Instruction(program, () => {
                    Variable parameter = program.VariableController.GetVariable(parameterVariableName);
                    program.VariableController.SetVariable(parameterVariableName, parameter);
                });
        }
        */
        private static Instruction getInstructionToSetParameterToValue(ProgramMemory program, string parameterName, object value, Function function){
            return new Instruction(program, () => {
                    Variable parameter = function.GetParameter(0);
                    function.SetParameter(parameterName, parameter);
                });
        }

        public static string GetFunctionName(string line, string preSignature)
        {
            string functionName = new StringHelper(line).GetSubstringBetweenIndentifiers(preSignature, "(");
                    
            return functionName;
        }


    }
}