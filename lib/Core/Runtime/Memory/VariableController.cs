using System.Diagnostics;
using AInterpreter.Core.Logger;
using AInterpreter.Exceptions;

namespace AInterpreter.Core.Runtime
{
    /*

        The VariableController class is used to store global variables populated by the interpreter.
        These global variables will then be manipulated at the runtime of an interpreted .Ab program.

    */

    public class VariableController
    {
        private List<Variable> variables;
        public VariableController()
        {
            variables = new List<Variable>();
        }

        public Variable GetVariable(string variableName)
        {
            foreach (Variable variable in variables)
            {
                if(variable.VariableName.Equals(variableName))
                {
                    return variable;
                }
            }
            DebugLog.Log($"Could not find variable '{variableName}' | Throwing VariableNotFoundException!", DebugLog.LogType.ERROR);
            throw new VariableNotFoundException($"Could not find variable '{variableName}'");
        }

        // Add a new integer variable to the memory
        public void AddVariable(string variableName, int value)
        {
            foreach (Variable variable in variables)
            {
                if(variable.VariableName == variableName)
                {
                    DebugLog.Log($"Variable '{variableName}' alraedy exists! Did not add new variable.", DebugLog.LogType.WARNING);
                    return;
                }
            }
            DebugLog.Log($"Variable '{variableName}'={value} added to memory!", this);
            this.variables.Add(new Variable(variableName, value));
        }
        
        // Add a new string variable to the memory
        public void AddVariable(string variableName, string value)
        {
            foreach (Variable variable in variables)
            {
                if(variable.VariableName == variableName)
                {
                    DebugLog.Log($"Variable '{variableName}' alraedy exists! Did not add new variable.", DebugLog.LogType.WARNING);
                    return;
                }
            }
            DebugLog.Log($"Variable '{variableName}'={value} added to memory!", this);
            this.variables.Add(new Variable(variableName, value));
        }
        
        // Set integer value.
        public void SetVariable(string variableName, int value)
        {
            foreach (Variable variable in variables)
            {
                if(variable.VariableName == variableName)
                {
                    variable.SetValue(value);
                    DebugLog.Log($"Found variable {variableName}  and changed contents to '{value}'", this);
                    return;
                }
            }
            throw new VariableNotFoundException($"Can not change contents of '{variableName}' to: '{value}' | Variable does not exist!");
        }

        // Set string value.
        public void SetVariable(string variableName, string value)
        {
            foreach (Variable variable in variables)
            {
                if(variable.VariableName == variableName)
                {
                    variable.SetValue(value);
                    DebugLog.Log($"Found variable {variableName}  and changed contents to '{value}'", this);
                    return;
                }
            }
            throw new VariableNotFoundException($"Can not change contents of '{variableName}' to: '{value}' | Variable does not exist!");
        }
    }
}