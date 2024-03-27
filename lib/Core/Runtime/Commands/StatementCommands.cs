using AInterpreter.lib.Core.Runtime.Program;

namespace AInterpreter.lib.Core.Runtime.Commands
{
    public static class StatementCommands
    {


        /*------------------------------------
                    LESS THAN (2 variables)
        ------------------------------------*/ 
        public static void LessThan(ProgramMemory programMemory, string variableNameA, string variableNameB, string funcNameTrue, string funcNameFalse)
        {
            int variableAValue = programMemory.VariableController.GetVariable(variableNameA).GetIntValue();
            int variableBValue = programMemory.VariableController.GetVariable(variableNameB).GetIntValue();

            LessThan(programMemory, variableAValue, variableBValue, funcNameTrue, funcNameFalse);
        }

        /*------------------------------------
                    LESS THAN (1 variable, 1 value)
        ------------------------------------*/ 
        public static void LessThan(ProgramMemory programMemory, string variableNameA, int? valueB, string funcNameTrue, string funcNameFalse)
        {
            int variableAValue = programMemory.VariableController.GetVariable(variableNameA).GetIntValue();

            LessThan(programMemory, variableAValue, valueB, funcNameTrue, funcNameFalse);
        }

        /*------------------------------------
                    LESS THAN (2 values)
        ------------------------------------*/ 
        public static void LessThan(ProgramMemory programMemory, int? valueA, int? valueB, string funcNameTrue, string funcNameFalse)
        {
            if(valueA < valueB)
            {
                programMemory.AddFunctionToExecutionStack(funcNameTrue);
            }
            else
            {
                programMemory.AddFunctionToExecutionStack(funcNameFalse);
            }
        }
    }
}