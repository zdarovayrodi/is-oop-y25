using Backups.Exceptions;

namespace Backups.Extra.Logger;

public class Logger
{
    private ILogger _instance;

    public Logger(ILogger instance)
    {
        _instance = instance ?? throw new BackupException("Logger instance can't be null");
    }

    public void Log(string message)
    {
        _instance.Log(message);
    }
}