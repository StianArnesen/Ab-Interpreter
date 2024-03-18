using AInterpreter.Core.Signatures;
using AInterpreter.Core.Runtime.Commands;
using AInterpreter.Exceptions;
using AInterpreter.Core.Runtime;
using AInterpreter.Core.Configuration;

namespace AInterpreter.Interpreter
{
    static class ConsoleCommandsInterpreter
    {
        /*
            ConsoleCommandsInterpreter.GetInstructionSet():
                Only run if the current line of code starts with ConsoleSignatures.CONSOLE_COMMAND_STARTS_WITH_SIGNATURE;
        */

        public static Instruction GetInstructionSet(ProgramMemory program, string line)
        {
            line = line.Trim();
            line = line.Replace(ConsoleSignatures.CONSOLE_COMMAND_CLASS, "");
           
            string consoleFunctionName = getSystemFunctionNameFromLine(line);

            switch (consoleFunctionName)
            {
                case ConsoleSignatures.CONSOLE_COMMAND_OUTPUT_CLASS:
                {
                    line = line.Replace(ConsoleSignatures.CONSOLE_COMMAND_OUTPUT_CLASS, "");
                    string outputType = getConsoleCommandOutputType(line);

                    switch (outputType)
                    {
                        case ConsoleSignatures.CONSOLE_COMMAND_OUTPUT_INFO:
                        {
                            return getInstructionSetForOutFunction(program, line, ConsoleConfiguration.CONSOLE_COLOR_WARNING);
                        }
                        case ConsoleSignatures.CONSOLE_COMMAND_OUTPUT_WARNING:
                        {
                            return getInstructionSetForOutFunction(program, line, ConsoleConfiguration.CONSOLE_COLOR_WARNING);
                        }
                        case ConsoleSignatures.CONSOLE_COMMAND_OUTPUT_ERROR:
                        {
                            return getInstructionSetForOutFunction(program, line, ConsoleConfiguration.CONSOLE_COLOR_ERROR);
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
                case ConsoleSignatures.CONSOLE_COMMAND_INPUT_CLASS:
                {
                    return new Instruction(program, () => ConsoleCommands.GetInputFromConsole());
                }

            }
            throw new InvalidSyntaxException($"Could not find any suitable console instructions from: {line}", program.CurrentLineNumber);
        }
        
        private static string getSystemFunctionNameFromLine(string line)
        {
            return new StringHelper(line).FromStartToNextCharacter('.');
        }

        private static Instruction getInstructionSetForOutFunction(ProgramMemory program, string line, ConsoleColor consoleColor = ConsoleColor.White)
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