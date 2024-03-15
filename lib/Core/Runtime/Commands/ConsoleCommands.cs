namespace AInterpreter.Core.Runtime.Commands
{
    public static class ConsoleCommands
    {
        public static void PrintToConsole(string? text, ConsoleColor color)
        {
            if(text == null)
            {
                return;
            }
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void ClearConsole()
        {
            Console.Clear();
        }
        public static string GetInputFromConsole()
        {
            string? userInput = Console.ReadLine();
            return (userInput != null) ? userInput : "";
        }
    }
}