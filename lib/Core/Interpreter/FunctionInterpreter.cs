using AInterpreter.lib.Core.Logger;
using AInterpreter.lib.Core.Runtime.Memory.Functions;
using AInterpreter.lib.Core.Runtime.Memory.Instructions;
using AInterpreter.lib.Core.Runtime.Memory.Variables;
using AInterpreter.lib.Core.Runtime.Program;
using AInterpreter.lib.Core.Signatures;

namespace AInterpreter.lib.Core.Interpreter
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
            // Fra hvordan jeg leser denne er det ikke mulig å bruke resultatet av en funksjon i parameterlisten
            // f.eks sqrt(pow(2,2))
            string[] parametersString = line.Split("(")[1].Split(")")[0].Split(",");
            
            foreach (string parameterName in parametersString)
            {
                // Ganske løs sjekk på gyldigheten av parameternavn, symboler og greier kan kanskje fungere? :P
                // functionName(/-+, --)
                if(parameterName.Trim().Equals(""))
                {
                    continue;
                }
                DebugLog.Log($"Adding parameter: {parameterName} to {function.Name}()", DebugLog.LogType.INFO);
                programMemory.VariableController.AddVariable(parameterName, "str_null");
            }
        }
        
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