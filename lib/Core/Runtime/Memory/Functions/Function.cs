using AInterpreter.lib.Core.Logger;
using AInterpreter.lib.Core.Runtime.Memory.Instructions;
using AInterpreter.lib.Core.Runtime.Memory.Variables;
using AInterpreter.lib.Exceptions;

namespace AInterpreter.lib.Core.Runtime.Memory.Functions
{
    public class Function
    {
        private List<Instruction> instructionList;
        private List<Variable>? parameters;
        public string Name { get; private set; }

        public Function(string name)
        {
            this.Name = name;
            this.instructionList = new List<Instruction>();
        }
        public Function(string name, List<Variable> parameters)
        {
            this.Name = name;
            this.parameters = parameters;
            this.instructionList = new List<Instruction>();
        }
        public List<Instruction> GetInstructions()
        {
            List<Instruction> reversedList = new List<Instruction>(instructionList);
            reversedList.Reverse();
            
            return reversedList;
        }

        public Variable GetParameter(string parameterName)
        {
            if (parameters == null)
            {
                DebugLog.Log($"No parameters named '{parameterName}' found for function: '{this.Name}'! This function has no parameters!", DebugLog.LogType.ERROR);
                throw new VariableNotFoundException($"No parameters named '{parameterName}' found for function: '{this.Name}'!", this);
            }
            foreach (Variable parameter in parameters)
            {
                if (parameter.VariableName == parameterName)
                {
                    return parameter;
                }
            }
            DebugLog.Log($"No parameters named '{parameterName}' found for function: '{this.Name}'!", DebugLog.LogType.ERROR);
            throw new VariableNotFoundException($"No parameters named '{parameterName}' found for function: '{this.Name}'!", this);
        }

        public Variable GetParameter(int parameterIndex)
        {
            if (parameters == null)
            {
                DebugLog.Log($"No parameters found for function: '{this.Name}'! This function has no parameters @GetParameter(int index)!", DebugLog.LogType.ERROR);
                throw new VariableNotFoundException($"No parameters with idnex '{parameterIndex}' found for function: '{this.Name}'!");    
            }
            return parameters[parameterIndex];
        }
        public void SetParameter(string parameterName, Variable parameter)
        {
            if (parameters == null)
            {
                parameters = new List<Variable>();
            }
            foreach (Variable param in parameters)
            {
                if (param.VariableName == parameterName)
                {
                    param.SetValue(parameter.GetIntValue());
                    return;
                }
            }
            DebugLog.Log($"No parameters named '{parameterName}' found for function: '{this.Name}'!", DebugLog.LogType.ERROR);
            throw new VariableNotFoundException($"No parameters named '{parameterName}' found for function: '{this.Name}'!");
        }

        public void SetParameter(int index, Variable parameter)
        {
            if (parameters == null)
            {
                parameters = new List<Variable>();
            }
            parameters[index] = parameter;
            string paramName = parameters[index].VariableName;

            DebugLog.Log($"Set parameter '{paramName}'[Index: {index}] to '{parameter.GetValuesToString()}' for function: '{this.Name}'", this);
        }

        public void AddParameter(Variable parameter)
        {
            if (parameters == null)
            {
                parameters = new List<Variable>();
            }
            parameters.Add(parameter);
        }
        
        public List<Instruction> GetInstructionStack()
        {
            return instructionList;
        }
        
        public void AddInstruction(Instruction instruction)
        {
            instructionList.Add(instruction);
        }

        public void AddInstructions(List<Instruction> instructions)
        {
            foreach (Instruction instruction in instructions)
            {
                AddInstruction(instruction);
            }
        }

        public void Run()
        {
            /*foreach (AInstruction instruction in instructionStack)
            {
                instruction.Execute();
            }
            */
        }
    }
}