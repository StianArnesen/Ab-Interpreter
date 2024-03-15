namespace AInterpreter.Core.Runtime.Commands
{
    public static class MathOperations
    {
        public static Action Add(ProgramMemory programMemory, string leftHandVariableName, string rightHandVariableName)
        {
            return new Action(() => 
            {
                int leftHandValue = int.Parse(programMemory.VariableController.GetVariable(leftHandVariableName).Value.ToString());
                int rightHandValue = int.Parse(programMemory.VariableController.GetVariable(rightHandVariableName).Value.ToString());
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