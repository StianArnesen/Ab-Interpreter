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
            
        }
       
    }
}
