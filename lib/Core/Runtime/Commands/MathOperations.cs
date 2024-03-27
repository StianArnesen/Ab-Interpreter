using AInterpreter.lib.Core.Runtime.Program;

namespace AInterpreter.lib.Core.Runtime.Commands
{
    public static class MathOperations
    {
        public static Action Add(ProgramMemory programMemory, string leftHandVariableName, string rightHandVariableName)
        {
            return new Action(() => 
            {
                int leftHandValue = programMemory.VariableController.GetVariable(leftHandVariableName).GetIntValue();
                int rightHandValue = programMemory.VariableController.GetVariable(rightHandVariableName).GetIntValue();
                int result = leftHandValue + rightHandValue;
                
                programMemory.VariableController.SetVariable(leftHandVariableName, result);
            });
        }
        
        public static int Subtract(int a, int b)
        {
            return a - b;
        }
        public static int Multiply(int a, int b)
        {
            return a * b;
        }
        public static int Divide(int a, int b)
        {
            return a / b;
        }
    }
}