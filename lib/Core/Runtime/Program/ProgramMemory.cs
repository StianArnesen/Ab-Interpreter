using System.Runtime.Serialization;
using AInterpreter.Core.Logger;
using AInterpreter.Core.Signatures;
using AInterpreter.Exceptions;

namespace AInterpreter.Core.Runtime
{
    public class ProgramMemory
    {
        public List<Function> functionList = new List<Function>(); // Contains list of functions which contains instructions to execute if added to execution stack.
        public VariableController VariableController = new VariableController();
        private Stack<Function> executionStack = new Stack<Function>(); // Used to store functions that are currently being executed.'        
        public int CurrentLineNumber {get; set;} = 1;
        public bool IsFirstTimeRunning = true;
        public bool IsEndOfApplication {get; private set;} = false;
        private string CurrentFunctionName = "";

        public ProgramMemory()
        {
            
        }
        public void EndApplication(object source)
        {
            DebugLog.Log("End of application!", this);
            IsEndOfApplication = true;
        }
        public void SetCurrentFunctionName(string name)
        {
            DebugLog.Log($"Setting current function to: {name}", this);
            CurrentFunctionName = name;
        }
        private bool doesFunctionExist(string name)
        {
            foreach (Function func in functionList)
            {
                if(func.Name.CompareTo(name) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        
        public Function? GetCurrentFunction()
        {
            return GetFunctionByName(CurrentFunctionName);
        }

        public void RemoveCurrentFunction()
        {
            Function? currentFunction = GetCurrentFunction();
            if (currentFunction != null)
            {
                functionList.Remove(currentFunction);
            }
        }

        public void AddFunction(Function function)
        {
            if(doesFunctionExist(function.Name))
            {
                DebugLog.Log($"Function {function.Name}() already exists!", DebugLog.LogType.ERROR);
                return;
            }
            DebugLog.Log($"Adding function: {function.Name}()", this);
            this.CurrentFunctionName = function.Name;
            this.functionList.Add(function);
        }
        
        public Function GetFunctionByName(string name)
        {
            if(functionList.Count < 1)
            {
                DebugLog.Log("Could not find function! FunctionList is empty!", DebugLog.LogType.ERROR);
                throw new EndOfQueueException("End of application");
            }
            foreach (Function func in functionList)
            {
                if(func.Name.CompareTo(name) == 0)
                {
                    return func;
                }
            }
            DebugLog.Log($"Could not find function {name}()", DebugLog.LogType.ERROR);
            throw new FunctionNotFoundException(name);
        }

        public bool AddInstructionToCurrentFunction(Instruction? instruction)
        {
            if(instruction == null){
                DebugLog.Log("Could not add new instruction. Instruction is null!", DebugLog.LogType.ERROR, this);
                return false;
            }
            Function? currentFunction = GetCurrentFunction();
            
            if(currentFunction == null)
            {
                DebugLog.Log("Could not add new instruction. No active function!", DebugLog.LogType.ERROR, this);
                return false;
            }
            currentFunction.AddInstruction(instruction);
            return true;
        }

        public bool AddInstructionsToCurrentFunction(List<Instruction> instructions)
        {
            if(instructions == null){
                DebugLog.Log("Could not add instructions. Instructions is null!", DebugLog.LogType.ERROR, this);
                return false;
            }
            Function? currentFunction = GetCurrentFunction();
            
            if(currentFunction == null)
            {
                DebugLog.Log("Could not add new instruction. No active function!", DebugLog.LogType.ERROR, this);
                return false;
            }
            currentFunction.AddInstructions(instructions);
            return true;
        }

        public Function? GetNextFunctionToExecute()
        {
            if(executionStack == null || executionStack.Count < 1)
            {
                EndApplication(this);
                return null;
            }

            return executionStack.Pop();
        }

        public void AddFunctionToExecutionStack(Function function)
        {
            executionStack.Push(function);
        }
        
        public void AddFunctionToExecutionStack(string functionName)
        {
            Function function = GetFunctionByName(functionName);
            executionStack.Push(function);
        }
        
    }
}