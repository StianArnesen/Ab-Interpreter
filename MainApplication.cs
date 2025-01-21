using System.Diagnostics;
using AInterpreter.lib.Core.Interpreter;
using AInterpreter.lib.Core.Logger;

// Denne stilen av namespacing er nicere, mindre indentering
namespace AInterpreter;

// Konvensjon for namespacing er at det følger mappestruktur, så hvis f.eks Program skal ligge under lib.Core.Runtime.Program bør det namespaces deretter
using Program = AInterpreter.lib.Core.Runtime.Program.Program; // Her så kræsjer Program-klassenavnet med Program.cs så måtte hive inn denne

class MainApplication
{
    // Ser ikke grunn til å la program være protected, konvensjon er private variablers navn starter med _
    private readonly Program _program;

    public MainApplication(string[] args)
    {
        FileInfo fileToInterpret = new FileInfo(args.Length < 1? "C:/Users/stian/OneDrive/Documents/C_Sharp_projects/AInterpreter/A-Flat-Projects/hello_world/Aflat_example_program.ab" : args[0]);
        
        this._program = new Program();
        new Interpreter(fileToInterpret, _program.ProgramMemory);

        this.runProgram();
    }
    
    public static void Main(string[] args)
    {
        // Småpirk: Konvensjon i C# er PascalCase for funksjonsnavn
        logArguments(args); 
        // Skjønner ikke helt behovet for å opprette en instans av MainApplication, kan ikke det som gjøres i konstruktøren dette gjøres rett her eller som en funksjon? 
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