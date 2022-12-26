namespace Backups.Extra.Logger;

public class FileLogger : ILogger
{
    private readonly string _logFilePath;

    public FileLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public void Log(string message)
    {
        File.AppendAllText(_logFilePath, message + Environment.NewLine);
    }
}