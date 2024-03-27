using System.Diagnostics;
using AInterpreter.lib.Core.Interpreter;
using AInterpreter.lib.Core.Logger;

namespace AInterpreter;

namespace AInterpreter 
{
    class MainApplication
    {
        protected Program program;
using Program = AInterpreter.lib.Core.Runtime.Program.Program; // Her så kræsjer Program-klassenavnet med Program.cs så måtte hive inn denne

class MainApplication
{
    private readonly Program _program;

        public MainApplication(string[] args)
        {
            FileInfo fileToInterpret = new FileInfo(args.Length < 1? "C:/Users/stian/OneDrive/Documents/C_Sharp_projects/AInterpreter/A-Flat-Projects/hello_world/Aflat_example_program.ab" : args[0]);
            
            this.program = new Program();
            new Interpreter.Interpreter(fileToInterpret, program.ProgramMemory);
    public MainApplication(string[] args)
    {
        FileInfo fileToInterpret = new FileInfo(args.Length < 1? "C:/Users/stian/OneDrive/Documents/C_Sharp_projects/AInterpreter/A-Flat-Projects/hello_world/Aflat_example_program.ab" : args[0]);
        
        this._program = new Program();
        new Interpreter(fileToInterpret, _program.ProgramMemory);

            this.runProgram();
        }
        
        public static void Main(string[] args)
        {
            logArguments(args);
            new MainApplication(args);
        }
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

        while(_program.IsRunning)
        {
            _program.Execute();   
        }
        
        executionTimer.Stop();
        var elapsedMs = executionTimer.ElapsedMilliseconds;

        Console.WriteLine($"Execution complete!  Execution took: {elapsedMs} ms");
    }
    
}