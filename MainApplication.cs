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
            FileInfo fileToInterpret = new FileInfo(args.Length < 1? "C:/Users/stian/OneDrive/Documents/C_Sharp_projects/AInterpreter/A-Flat-Projects/hello_world/Aflat_example_program.ab" : args[0]);
            
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

            while(program.IsRunning)
            {
                program.Execute();   
            }
            
            executionTimer.Stop();
            var elapsedMs = executionTimer.ElapsedMilliseconds;

            Console.WriteLine($"Execution complete!  Execution took: {elapsedMs} ms");
        }
        
    }
}