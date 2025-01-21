using AInterpreter.lib.Core.Runtime.Memory.Instructions;
using AInterpreter.lib.Core.Signatures;

namespace AInterpreter.lib.Core.Runtime.Program
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
