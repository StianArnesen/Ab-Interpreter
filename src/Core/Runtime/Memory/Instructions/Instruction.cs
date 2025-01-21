/*
    * AInstruction.cs
    * Description: Used to store and execute a single function stored as an Action.
    *
*/

using System.Reflection;
using AInterpreter.lib.Core.Logger;
using AInterpreter.lib.Core.Runtime.Program;

namespace AInterpreter.lib.Core.Runtime.Memory.Instructions
{   
    public class Instruction
    {
        private Action instruction;     // The instruction to be executed.
        public int LineNumber {get;}    // Where this instruction is located in the .Ab file.

        public Instruction(ProgramMemory program, Action instruction)
        {
            this.instruction = instruction;
            this.LineNumber  = program.CurrentLineNumber;
        }
        
        public void Execute()
        {
            DebugLog.Log($"Executing instruction at line {LineNumber}", this);
            instruction();
        }
        public string GetInstructionInfo()
        {
            return $"At line {LineNumber} action = {instruction.GetMethodInfo()} From {instruction.Target}";
        }
    }
}