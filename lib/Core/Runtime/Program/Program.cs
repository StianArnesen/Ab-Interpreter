using AInterpreter.Core.Logger;
using AInterpreter.Core.Signatures;
using AInterpreter.Exceptions;

namespace AInterpreter.Core.Runtime
{
    public class Program
    {
        public bool IsRunning {get; protected set;} = true;
        public ProgramMemory ProgramMemory;
        public Program()
        {
            ProgramMemory = new ProgramMemory();
        }

        public virtual void Execute()
        {
            if(this.ProgramMemory.IsFirstTimeRunning)
            {
                ProgramMemory.AddFunctionToExecutionStack(ProgramMemory.GetFunctionByName(FunctionSignatures.FUNCTION_ENTRY_NAME));
                this.ProgramMemory.IsFirstTimeRunning = false;
            }
            if(ProgramMemory.IsEndOfApplication)
            {
                IsRunning = false;
                return;
            }
            
            Instruction? instruction = ProgramMemory.NextInstruction();
            instruction?.Execute();

            ProgramMemory.CurrentLineNumber = (instruction == null)? ProgramMemory.CurrentLineNumber : instruction.LineNumber;
        }
       
    }
}
