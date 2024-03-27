using AInterpreter.lib.Core.Configuration;
using AInterpreter.lib.Core.Runtime.Commands;
using AInterpreter.lib.Core.Runtime.Memory.Instructions;
using AInterpreter.lib.Core.Runtime.Program;
using AInterpreter.lib.Core.Signatures;
using AInterpreter.lib.Exceptions;

namespace AInterpreter.lib.Core.Interpreter
{
    static class ConsoleCommandsInterpreter
    {
        /*
            ConsoleCommandsInterpreter.GetInstructionSet():
                Only run if the current line of code starts with ConsoleSignatures.CONSOLE_COMMAND_STARTS_WITH_SIGNATURE;
        */

        public static Instruction GetInstruction(ProgramMemory program, string line)
        {
            line = line.Trim();
            line = line.Replace(ConsoleSignatures.CONSOLE_COMMAND_CLASS + GlobalSignatures.OBJECT_SEPERATOR, "");
           
            string consoleFunctionName = getSystemFunctionNameFromLine(line);

            switch (consoleFunctionName)
            {
                case ConsoleSignatures.CONSOLE_COMMAND_OUTPUT_CLASS:
                {
                    return getInstructionForOutFunction(program, line);
                }
                case ConsoleSignatures.CONSOLE_COMMAND_INPUT_CLASS:
                {
                    return getInstructionForInputFunction(program, line);
                }

            }
            throw new InvalidSyntaxException($"Could not find any suitable console instructions from: {line}", program.CurrentLineNumber);

        }

        private static Instruction getInstructionForInputFunction(ProgramMemory program, string line)
        {
            string inputType = new StringHelper(line).GetSubstringBetweenChars('.', '(');
            string variableNameToSet = new StringHelper(line).GetSubstringBetweenChars('(', ')');

            switch (inputType)
            {
                case ConsoleSignatures.CONSOLE_COMMAND_INPUT_INTEGER:
                {
                    return new Instruction(program, () => program.VariableController.SetVariable(variableNameToSet, int.Parse(ConsoleCommands.GetInputFromConsole())));
                }
                case ConsoleSignatures.CONSOLE_COMMAND_INPUT_STRING:
                {
                    return new Instruction(program, () => program.VariableController.SetVariable(variableNameToSet, ConsoleCommands.GetInputFromConsole()));
                }
                default:
                {
                    throw new InvalidSyntaxException($"Input of type '{inputType}' is not supported.", program.CurrentLineNumber);
                }
            }

        }
        
        private static string getSystemFunctionNameFromLine(string line)
        {
            string systemFunctionName = new StringHelper(line).FromStartToNextCharacter(GlobalSignatures.OBJECT_SEPERATOR);
            if(systemFunctionName == null || systemFunctionName.Length < 1)
            {
                return new StringHelper(line).FromStartToNextCharacter(GlobalSignatures.OPEN_PARENTHESIS);;
            }
            return systemFunctionName;
        }

        private static Instruction getInstructionForOutFunction(ProgramMemory program, string line, ConsoleColor consoleColor = ConsoleColor.White)
        {
            line = line.Replace($"{GlobalSignatures.OBJECT_SEPERATOR + ConsoleSignatures.CONSOLE_COMMAND_OUTPUT_CLASS}", "");
            string outputType = getConsoleCommandOutputType(line);

            switch (outputType)
            {
                case ConsoleSignatures.CONSOLE_COMMAND_OUTPUT_INFO:
                {
                    return getInstructionForOutput(program, line, ConsoleConfiguration.CONSOLE_COLOR_WARNING);
                }
                case ConsoleSignatures.CONSOLE_COMMAND_OUTPUT_WARNING:
                {
                    return getInstructionForOutput(program, line, ConsoleConfiguration.CONSOLE_COLOR_WARNING);
                }
                case ConsoleSignatures.CONSOLE_COMMAND_OUTPUT_ERROR:
                {
                    return getInstructionForOutput(program, line, ConsoleConfiguration.CONSOLE_COLOR_ERROR);
                }
                case ConsoleSignatures.CONSOLE_COMMAND_CLEAR_OUTPUT:
                {
                    return new Instruction(program, () => ConsoleCommands.ClearConsole());
                }
                default:
                {
                    throw new InvalidSyntaxException($"Could not find any suitable console instructions from: {line}", program.CurrentLineNumber);
                }
            }

        }

        private static Instruction getInstructionForOutput(ProgramMemory program, string line, ConsoleColor consoleColor)
        {
            /* Check if print statement is pure string or variable */
            string betweenParentheses = new StringHelper(line).GetSubstringBetweenChars('(', ')');
            string variableName = betweenParentheses;
            
            if(betweenParentheses.Contains('"'))
            {
                string stringToPrint = new StringHelper(betweenParentheses).GetSubstringBetweenChars('"', '"');
                return new Instruction(program, () => ConsoleCommands.PrintToConsole(stringToPrint, consoleColor));
            }
            
            /* A variable reference is assumed since the string did not contain any string markers. */
            return new Instruction(program, () => ConsoleCommands.PrintToConsole(program.VariableController.GetVariable(variableName).GetValuesToString(), consoleColor));
        }

        private static string getConsoleCommandOutputType(string line)
        {
            return new StringHelper(line).GetSubstringBetweenChars('.', '(');
        }

    }
}