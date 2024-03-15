using AInterpreter.Core.Runtime;
using AInterpreter.Core.Logger;

namespace AInterpreter
{
    class MainApplication
    {
        protected Program program;

        public MainApplication(string[] args)
        {
            FileInfo fileToInterpret = new FileInfo("C:/Users/stian/OneDrive/Documents/C_Sharp_projects/AInterpreter/A-Flat Projects/hello_world/hello_world.Ab");
            
            this.program = new DefaultProgram();
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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            while(program.IsRunning)
            {
                program.Execute();
            }
           
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Elapsed Time: {elapsedMs} ms");
        }
        
    }
}