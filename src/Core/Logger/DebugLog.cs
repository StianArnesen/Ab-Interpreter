
using AInterpreter.lib.Core.File;

namespace AInterpreter.lib.Core.Logger
{
    public static class DebugLog
    {
        public enum LogType
        {
            ERROR,
            WARNING,
            INFO
        }

        public static bool IsConsoleOutputEnabled { get; set; } = false;
        public static bool IsFileLoggerEnabled { get; set; } = false;

        public static void Log(string message, LogType logType, object source)
        {
            string currentTime = DateTime.Now.ToString("dd.MM mm:ss");
            string finalMessageToPrint = $"[{logType}] [@{source}]: {message}";

            switch (logType)
            {
                case LogType.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogType.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.INFO:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            if (IsConsoleOutputEnabled)
            {
                Console.WriteLine($"[{logType}]: {finalMessageToPrint}");
            }
            if (IsFileLoggerEnabled)
            {
                FileHandler.WriteToLog($"\n[{currentTime}]:      {finalMessageToPrint}"); ;
            }
        }

        public static void AddEmptyLine()
        {
            if (IsConsoleOutputEnabled)
            {
                Console.WriteLine();
            }
            if (IsFileLoggerEnabled)
            {
                FileHandler.WriteToLog(" "); ;
            }
        }

        public static void Log(string message, object source)
        {
            Log(message, LogType.INFO, source);
        }

    }
}