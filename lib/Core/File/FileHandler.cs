using System.Diagnostics;
using AInterpreter.Core.Logger;

public class FileHandler
{
    public static string LogDirectory {get;set;} = "logs";
    public static string? LogFilename {get; private set;}
    public static string? LogFilePath {get; private set;}


    public static string[] GetLinesFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found.", filePath);
        }

        string[] lines = File.ReadAllLines(filePath);
        
        return lines;
    }
    

    public static string CreateNewLogfile()
    {
        LogFilename = DateTime.Now.ToString("dd.MM.yyyy__HH_mm") + ".log";
        LogFilePath = Path.Combine(LogDirectory, LogFilename);

        using (StreamWriter writer = new StreamWriter(LogFilePath, true))
        {
            string message = "--------------------------START--OF--LOG--------------------------";
            writer.WriteLine($"\n[{DateTime.Now}]: Log created. \n{message}");
        }
        return LogFilename;
    }
    
    public static void WriteToLog(string line)
    {
        if(LogFilePath == null)
        {
            CreateNewLogfile();
        }
        try
        {
            if(LogFilePath == null){
                return;
            }
            if (!File.Exists(LogFilePath))
            {
                File.WriteAllText(LogFilePath, line);
            }
            else
            {
                File.AppendAllText(LogFilePath, line);
            }
        }
        catch (Exception ex)
        {
            DebugLog.Log($"Error writing to file: {ex.Message}", DebugLog.LogType.ERROR);
        }
    }
    
}