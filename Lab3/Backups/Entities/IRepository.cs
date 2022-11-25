namespace Backups.Entities
{
    /// <summary>
    /// Файл просто предоставляет доступ к какому-то объекту (стриму данных), на который указывает пользователь
    /// пример:
    /// Юзер: я хочу достать файл по такому-то пути
    /// Репа: отдает стрим
    /// Юзер: спасибо
    /// </summary>
    public interface IRepository
    {
        IReadOnlyList<string> Files { get; }
        IReadOnlyList<string> Directories { get; }
        void AddFile(string filePath);
        void AddDirectory(string directoryPath);
    }
}