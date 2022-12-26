{
  "RestorePoints": [
    {
      "$type": "Backups.Models.RestorePoint, Backups",
      "CreationDate": "2022-12-26T18:51:36.223218+03:00",
      "Name": "TestFIRST0",
      "BackupObjects": [
        {
          "$type": "Backups.Entities.BackupObject, Backups",
          "Name": "File",
          "Extension": "txt",
          "FullPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt",
          "FileBytes": "AAAAAAAAAAAAAAAAAAAAAA=="
        },
        {
          "$type": "Backups.Entities.BackupObject, Backups",
          "Name": "File1",
          "Extension": "txt",
          "FullPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File1.txt",
          "FileBytes": "AAAAAAAAAAAAAAAAAAAAAA=="
        }
      ],
      "Storages": [
        {
          "$type": "Backups.Models.Storage, Backups",
          "OriginalPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt",
          "BackupPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/TestFIRST0/File_0.zip",
          "Bytes": "AAAAAAAAAAAAAAAAAAAAAA=="
        },
        {
          "$type": "Backups.Models.Storage, Backups",
          "OriginalPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File1.txt",
          "BackupPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/TestFIRST0/File1_0.zip",
          "Bytes": "AAAAAAAAAAAAAAAAAAAAAA=="
        }
      ]
    },
    {
      "$type": "Backups.Models.RestorePoint, Backups",
      "CreationDate": "2022-12-26T18:51:36.227546+03:00",
      "Name": "TestSECOND1",
      "BackupObjects": [
        {
          "$type": "Backups.Entities.BackupObject, Backups",
          "Name": "File",
          "Extension": "txt",
          "FullPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt",
          "FileBytes": "AAAAAAAAAAAAAAAAAAAAAA=="
        },
        {
          "$type": "Backups.Entities.BackupObject, Backups",
          "Name": "File1",
          "Extension": "txt",
          "FullPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File1.txt",
          "FileBytes": "AAAAAAAAAAAAAAAAAAAAAA=="
        }
      ],
      "Storages": [
        {
          "$type": "Backups.Models.Storage, Backups",
          "OriginalPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt",
          "BackupPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/TestSECOND1/File_1.zip",
          "Bytes": "AAAAAAAAAAAAAAAAAAAAAA=="
        },
        {
          "$type": "Backups.Models.Storage, Backups",
          "OriginalPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File1.txt",
          "BackupPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/TestSECOND1/File1_1.zip",
          "Bytes": "AAAAAAAAAAAAAAAAAAAAAA=="
        }
      ]
    }
  ],
  "Algorithm": {
    "$type": "Backups.Models.SplitStorageAlgorithm, Backups"
  },
  "Name": "test",
  "FullPath": "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/"
}