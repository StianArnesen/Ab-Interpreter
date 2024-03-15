namespace AInterpreter.Core.Signatures
{
    public static class ConsoleSignatures
    {
        public const string CONSOLE_COMMAND_CLASS           = "Console.";
        public const string CONSOLE_COMMAND_OUTPUT_CLASS    = "out";
        public const string CONSOLE_COMMAND_INPUT_CLASS     = "in"; // Example: Console.in();
        public const string CONSOLE_COMMAND_OUTPUT_INFO     = "info"; // Example: Console.out.warning("This is a warning message!");
        public const string CONSOLE_COMMAND_OUTPUT_WARNING  = "warning"; // Example: Console.out.warning("This is a warning message!");
        public const string CONSOLE_COMMAND_OUTPUT_ERROR    = "error"; // Example: Console.out.error("This is an error message!");
        public const string CONSOLE_COMMAND_CLEAR_OUTPUT    = "clear"; // Example: Console.out.clear();


    }
}