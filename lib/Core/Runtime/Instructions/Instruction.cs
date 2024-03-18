/*
    * AInstruction.cs
    * Description: Used to store and execute a single function stored as an Action.
    *
*/

using System.Diagnostics;
using System.Reflection;
using AInterpreter.Core.Logger;

namespace AInterpreter.Core.Runtime
{   
    public class Instruction
    {
        private Action instruction;     // The instruction to be executed.
        public int LineNumber {get;}    // Where this instruction is located in the .Ab file.

        public Instruction(ProgramMemory program, Action instruction)
        {
            this.instruction = instruction;
            this.LineNumber   = program.CurrentLineNumber;
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