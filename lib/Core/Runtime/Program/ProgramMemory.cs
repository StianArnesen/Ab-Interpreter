using AInterpreter.Core.Logger;
using AInterpreter.Exceptions;

namespace AInterpreter.Core.Runtime
{
    public class ProgramMemory
    {
        public List<Function> functionList = new List<Function>(); // Contains list of functions which contains instructions to execute if added to execution stack.
        public VariableController VariableController = new VariableController();
        public Stack<Instruction> InstructionStack = new Stack<Instruction>(); // Used to store functions that are currently being executed.'

        public int CurrentLineNumber {get; set;} = 1;
        public bool IsFirstTimeRunning = true;
        public bool IsEndOfApplication {get; private set;} = false;
        private string CurrentFunctionName = "";

        public ProgramMemory()
        {
            
        }

        public Instruction? NextInstruction()
        {
            if(InstructionStack.Count < 1)
            {
                DebugLog.Log("No more instructions. Ending application.", DebugLog.LogType.WARNING, this);
                IsEndOfApplication = true;
                return null;
            }
            return InstructionStack.Pop();
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
                throw new EndOfQueueException("End of application", this);
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
            if(instruction == null)
            {
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

        public void addInstructionsFromFunctionToExecutionStack(Function? function)
        {
            List<Instruction> instructions = function.GetInstructions();
            foreach(Instruction instruction in instructions)
            {
                InstructionStack.Push(instruction);
                DebugLog.Log($"Added instruction to execution stack: {instruction.GetInstructionInfo()}", this);
            }
            if(instructions == null || instructions.Count < 1)
            {
                DebugLog.Log($"No instructions found for function: {function.Name}()", this);
            }
        }
        public void addInstructionsFromFunctionToExecutionStack(string functionName)
        {
            DebugLog.Log($"Adding instructions from function: {functionName}()", this);
            Function? function = GetFunctionByName(functionName);
            addInstructionsFromFunctionToExecutionStack(function);
        }


        public void AddFunctionToExecutionStack(Function function)
        {
            addInstructionsFromFunctionToExecutionStack(function);
            DebugLog.Log($"Added function {function.Name}(); to execution stack!", this);
        }
        
        public void AddFunctionToExecutionStack(string functionName)
        {
            Function function = GetFunctionByName(functionName);
            AddFunctionToExecutionStack(function);
        }
        
    }
}