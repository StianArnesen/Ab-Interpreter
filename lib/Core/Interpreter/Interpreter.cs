using AInterpreter.Core;
using AInterpreter.Core.Runtime;
using AInterpreter.Core.Logger;
using AInterpreter.Core.Signatures;
using AInterpreter.Core.Runtime.Commands;
using System.Diagnostics;

namespace AInterpreter.Interpreter
{
    class Interpreter
    {
        public int CurrentLineNumber {get; private set;} = 0;
        private List<Function> functionList = new List<Function>(); // Used to store all functions when interpreting.
        
        public Interpreter(FileInfo fileToInterpret, ProgramMemory programMemory)
        {            
            DebugLog.Log($"---------------- Interpreting '{fileToInterpret.Name} ' -----------------", this);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            interpretFile(fileToInterpret, programMemory);
            
            stopwatch.Stop();
            long elapsedTime = stopwatch.ElapsedMilliseconds;
            
            DebugLog.AddEmptyLine();
            DebugLog.Log("---------------- Interpreting done! -----------------", this);
            DebugLog.Log($"              Interpreting took {elapsedTime} ms     ", this);
            DebugLog.Log("------------------------------------------------------", this);
            DebugLog.AddEmptyLine();
        }

        private void interpretFile(FileInfo fileToInterpret, ProgramMemory programMemory)
        {
            string[] codeLines = FileHandler.GetLinesFromFile(fileToInterpret.FullName);

            FunctionInterpreter.InterpretAllFunctionDefinitionSignatures(codeLines, programMemory);
            interpretLines(codeLines, programMemory);
        }

        public void AddFunction(Function function)
        {
            //Check if function already exists.
            foreach (Function existingFunction in functionList)
            {
                if(existingFunction.Name == function.Name)
                {
                    DebugLog.Log($"Function with name {function.Name} already exists!", DebugLog.LogType.ERROR);
                    throw new Exception($"Function with name {function.Name} already exists!");
                }
            }
            functionList.Add(function);
        }

        private void interpretLines(string[]? codeLines, ProgramMemory programMemory)
        {
            if(codeLines == null){ return; }
            
            foreach (string line in codeLines)
            {
                interpretLine(line, programMemory);
                CurrentLineNumber++;
                programMemory.CurrentLineNumber++;
            }
        }

        private void interpretLine(string line, ProgramMemory programMemory)
        {
            line = line.Trim();

            if(line.Length < 2) { return; }; // Skip this line if its empty or only contains a single character.

            if(line.StartsWith(ConsoleSignatures.CONSOLE_COMMAND_CLASS))
            {
                interpretConsoleSignature(programMemory, line);
            }
            else if(line.Contains(FunctionSignatures.FUNCTION_EXECUTION_SIGNATURE))
            {
                FunctionInterpreter.InterpretExecutionSignature(programMemory, line);
            }
            else if(line.StartsWith(FunctionSignatures.RETURN_DEFINITION_SIGNATURE))
            {
                FunctionInterpreter.InterpretReturnSignature(programMemory, line);
            }
            else if(line.StartsWith(FunctionSignatures.FUNCTION_DEFINITION_SIGNATURE))
            {
                /* 
                    The function definition has already been handled in the constructor. 
                    Now we just need to set the current function name so that we can add instructions to it.
                */
                programMemory.SetCurrentFunctionName(FunctionInterpreter.GetFunctionName(line, FunctionSignatures.FUNCTION_DEFINITION_SIGNATURE));
            }
            else if(line.StartsWith(StatementSignatures.IF_STATEMENT_START))
            {
                StatementInterpreter.HandleIfStatementSignature(programMemory, line);
            }
            else if (line.Contains(OperatorSignatures.OPERATOR_CHARACTERS))
            {
                OperatorInterpreter.InterpretOperatorLine(programMemory, line);
            }
            else if (line.Contains(VariableSignatures.INTEGER_INITIALIZE) || line.Contains(VariableSignatures.STRING_INITIALIZE))
            {
                VariableInterpreter.interpretVariableSignature(programMemory, line);
            }
            else
            {   // Assuming line is variable if nothing else matches.
                VariableInterpreter.interpretVariableSignature(programMemory, line);
            }
        }

        private void interpretConsoleSignature(ProgramMemory programMemory, string line)
        {
            DebugLog.Log($"Console command detected in line: {line}! Interpreting...", this);
            Instruction instructionSet = ConsoleCommandsInterpreter.GetInstruction(programMemory, line);
            programMemory.AddInstructionToCurrentFunction(instructionSet);            
        }


    }
}