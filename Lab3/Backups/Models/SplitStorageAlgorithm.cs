// namespace Backups.Models
// {
//     using System.IO.Compression;
//     using Backups.Entities;
//     using Backups.Exceptions;
//
//     public class SplitStorageAlgorithm : IAlgorithm
//     {
//         private readonly IRepository _repository = new Repository();
//         public byte[] Data { get; private set; } = Array.Empty<byte>();
//         public void Compress(IRestorePoint restorePoint, int id)
//         {
//             var compressedDataStream = new MemoryStream();
//             using (compressedDataStream)
//             {
//                 foreach (IBackupObject backupObject in restorePoint.BackupObjects)
//                 {
//                     using (var archive = new ZipArchive(compressedDataStream, ZipArchiveMode.Update, false))
//                     {
//                         if (!string.IsNullOrEmpty(backupObject.Extension))
//                         {
//                             ZipArchiveEntry entry = archive.CreateEntry(backupObject.Name + " " + id + "." + backupObject.Extension);
//                             var originalFileStream = new MemoryStream(_repository.GetFile(backupObject.FullPath));
//                             using Stream entryStream = entry.Open();
//                             originalFileStream.CopyTo(entryStream);
//                         }
//                         else
//                         {
//                             ZipArchiveEntry entry = archive.CreateEntry(backupObject.Name + " " + id);
//                             using Stream? entryStream = entry.Open();
//                         }
//                     }
//                 }
//             }
//         }
//     }
// }