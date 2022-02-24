using System.IO.Compression;
using Backups.Backup;

namespace Backups.Repository
{
    public class Repository : IRepository
    {
        private string _pathToBackupingFiles;
        private string _backupDirectoryPath;

        public Repository(string pathToBackupingFiles, string backupDirectoryPath)
        {
            _pathToBackupingFiles = pathToBackupingFiles;
            _backupDirectoryPath = backupDirectoryPath;
        }

        public string PathToBackupingFiles => _pathToBackupingFiles;
        public RestorePoint SaveToConcreteRepo(List<Storage> storages, BackupJob backupJob)
        {
            if (backupJob is null)
                throw new BackupsException("Invalid backup job");
            if (storages is null)
                throw new BackupsException("Invalid storages");

            var restorePoint = new RestorePoint("RestorePoint");
            var fullPath = $"{_backupDirectoryPath}/{backupJob.Name}/{restorePoint.Name}";
            Directory.CreateDirectory(fullPath);
            CreateZipFiles(storages, fullPath);

            storages.ForEach(storage =>
            {
                restorePoint.AddFile(storage);
            });

            return restorePoint;
        }

        private void CreateZipFiles(List<Storage> storages, string directoryPath)
        {
           storages.ForEach(storage =>
           {
               var zipFilePath = $"{directoryPath}/{storage.ZipName}";
               if (!File.Exists(zipFilePath))
               {
                   using (var archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                   {
                       archive.CreateEntryFromFile(storage.PathToFileToSave,
                           Path.GetFileName(storage.PathToFileToSave));
                   }
               }
               else
               {
                   using (var zipToOpen = new FileStream(zipFilePath, FileMode.Open))
                   {
                       using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                       {
                           archive.CreateEntryFromFile(storage.PathToFileToSave, Path.GetFileName(storage.PathToFileToSave));

                       }
                   }
               }
           });
        }
        public void DeleteFromRepository(RestorePoint restorePoint, BackupJob backupJob)
        {
            if (restorePoint is null)
                throw new BackupsException("Restore Point cannot be null");
            if (backupJob is null)
                throw new BackupsException("Backup job cannot be null");
            
            Directory.Delete(_backupDirectoryPath + "/" + backupJob.Name + "/" + restorePoint.Name, true);
            backupJob.RemoveRestorePoint(restorePoint);
        }
    }
}