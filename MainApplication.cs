using AInterpreter.Core.Runtime;
using AInterpreter.Core.Logger;
using System.Diagnostics;

namespace AInterpreter
{
    class MainApplication
    {
        protected Program program;

        public MainApplication(string[] args)
        {
            FileInfo fileToInterpret = new FileInfo("C:/Users/stian/OneDrive/Documents/C_Sharp_projects/AInterpreter/A-Flat Projects/hello_world/hello_world.Ab");
            
            this.program = new Program();
            new Interpreter.Interpreter(fileToInterpret, program.ProgramMemory);

            this.runProgram();
        }
        
        public static void Main(string[] args)
        {
            logArguments(args);
            new MainApplication(args);
        }
        private static void logArguments(string[] args)
        {
            DebugLog.Log("Arguments:", "Class: MainApplication");
            foreach (string arg in args)
            {
                DebugLog.Log(arg, "Class: MainApplication");
            }
        }
        private void runProgram()
        {
            Stopwatch executionTimer = Stopwatch.StartNew();
            
            try
            {
                while(program.IsRunning)
                {
                    program.Execute();   
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            executionTimer.Stop();
            var elapsedMs = executionTimer.ElapsedMilliseconds;
            Console.WriteLine($"Execution complete!  Execution took: {elapsedMs} ms");
        }
        
    }
}