using Backups.Exceptions;
using Backups.Extra.Logger;
using Backups.Models;

namespace Backups.Extra.Entities;

public class BackupRestorer
{
    private Logger.Logger _logger;
    private IRestorePoint _restorePoint;

    public BackupRestorer(ILogger logger, IRestorePoint restorePoint)
    {
        _restorePoint = restorePoint;
        _logger = new Logger.Logger(logger);
    }

    public void SetRestorePoint(IRestorePoint restorePoint)
    {
        _restorePoint = restorePoint;
    }

    public void Restore(string? restoreFolderPath, bool toOriginalLocation = false)
    {
        if (restoreFolderPath == null && !toOriginalLocation)
            throw new BackupException("Wrong specimen of restore path");

        if (toOriginalLocation)
        {
            foreach (var file in _restorePoint.BackupObjects)
            {
                if (File.Exists(Path.Combine(file.FullPath, $"{file.Name}.{file.Extension}")))
                {
                    File.Delete(Path.Combine(file.FullPath, $"{file.Name}.{file.Extension}"));
                }
                else if (!Directory.Exists(file.FullPath))
                {
                    try
                    {
                        Directory.CreateDirectory(file.FullPath);
                    }
                    catch (Exception e)
                    {
                        _logger.Log($"Error: {e.Message}");
                        break;
                    }
                }

                File.WriteAllBytes(Path.Combine(file.FullPath, $"{file.Name}.{file.Extension}"), file.FileBytes);

                _logger.Log($"File {file.Name}.{file.Extension} was restored to original location");
            }
        }
        else
        {
            if (string.IsNullOrEmpty(restoreFolderPath))
                throw new BackupException("Wrong specimen of restore path");

            if (!Directory.Exists(restoreFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(restoreFolderPath);
                }
                catch (Exception e)
                {
                    _logger.Log($"Error: {e.Message}");
                }
            }

            foreach (var file in _restorePoint.BackupObjects)
            {
                File.WriteAllBytes(Path.Combine(restoreFolderPath, $"{file.Name}.{file.Extension}"), file.FileBytes);
            }

            _logger.Log($"Files were restored to {restoreFolderPath}");
        }
    }
}