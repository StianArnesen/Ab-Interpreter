using AInterpreter.Core.Logger;
using AInterpreter.Core.Signatures;

namespace AInterpreter.Core.Runtime
{
    public class DefaultProgram : Program
    {
        public DefaultProgram()
        {
            
        }
        public override void Execute()
        {
            if(this.ProgramMemory.IsFirstTimeRunning)
            {
                ProgramMemory.AddFunctionToExecutionStack(ProgramMemory.GetFunctionByName(FunctionSignatures.FUNCTION_ENTRY_NAME));
                this.ProgramMemory.IsFirstTimeRunning = false;
            }
            if(ProgramMemory.IsEndOfApplication)
            {
                IsRunning = false;
                DebugLog.Log("End of program!", this);
            }
            Function? nextFunction = ProgramMemory.GetNextFunctionToExecute();
            if(nextFunction == null)
            {
                IsRunning = false;
                DebugLog.Log("End of program!", this);
            }

            List<Instruction>? instructionList = nextFunction?.GetInstructionStack();
            if(instructionList == null)
            {
                DebugLog.Log($"No instructions found for function: {nextFunction?.Name}()!", this);
                return;
            }
            
            DebugLog.Log($"Executing function: {nextFunction?.Name}()", this);

            foreach (Instruction instruction in instructionList)
            {
                if(instruction == null)
                {
                    continue;
                }
                
                ProgramMemory.CurrentLineNumber = instruction.LineNumber;
                instruction.Execute();
            }

        }
    }
}