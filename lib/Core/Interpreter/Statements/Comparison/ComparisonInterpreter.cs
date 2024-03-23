using AInterpreter.Core.Interpreter.Statements.Comparison;
using AInterpreter.Core.Runtime;
using AInterpreter.Core.Runtime.Commands;
using AInterpreter.Exceptions;
using AInterpreter.Signatures;

namespace AInterpreter.Core.Interpreter.Statements
{
    class ComparisonInterpreter
    {
        public static Instruction GetComparisonInstruction(ProgramMemory programMemory, string variableNameA, string variableNameB, string funcNameTrue, string funcNameFalse, ComparisonOperator @operator, bool reversed)
        {
            return new Instruction(programMemory, getComparisonAction(programMemory, variableNameA, variableNameB, funcNameTrue, funcNameFalse, @operator, reversed));
        }

        public static Instruction GetComparisonInstruction(ProgramMemory programMemory, string variableNameA, int? valueB, string funcNameTrue, string funcNameFalse, ComparisonOperator @operator, bool reversed)
        {
            return new Instruction(programMemory, getComparisonAction(programMemory, variableNameA, valueB, funcNameTrue, funcNameFalse, @operator, reversed));
        }
        
        // Compare a variable with a value
        private static Action getComparisonAction(ProgramMemory programMemory, string variableNameA, int? valueB, string funcNameTrue, string funcNameFalse, ComparisonOperator @operator, bool reversed)
        {
            return () => {
                Variable variableA = programMemory.VariableController.GetVariable(variableNameA);

                if(variableA.Compare(valueB, @operator, reversed))
                {
                    programMemory.AddFunctionToExecutionStack(funcNameTrue);
                }
                else
                {
                    programMemory.AddFunctionToExecutionStack(funcNameFalse);
                }

            };
        }

        // Compare two variables
        private static Action getComparisonAction(ProgramMemory programMemory, string variableNameA, string variableNameB, string funcNameTrue, string funcNameFalse, ComparisonOperator @operator, bool reversed)
        {
            return () => {
                Variable variableA = programMemory.VariableController.GetVariable(variableNameA);
                Variable variableB = programMemory.VariableController.GetVariable(variableNameB);

                if(variableA.Compare(variableB, @operator, reversed))
                {
                    programMemory.AddFunctionToExecutionStack(funcNameTrue);
                }
                else
                {
                    programMemory.AddFunctionToExecutionStack(funcNameFalse);
                }

            };
        }
    }
}