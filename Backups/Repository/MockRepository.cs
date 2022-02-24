using Backups.Backup;

namespace Backups.Repository;

public class MockRepository : IRepository
{
    private string _pathToBackupingFiles;
    private string _backupDirectoryPath;

    public MockRepository(string pathToBackupingFiles, string backupDirectoryPath)
    {
        _pathToBackupingFiles = pathToBackupingFiles;
        _backupDirectoryPath = backupDirectoryPath;
    }
    
    public void DeleteFromRepository(RestorePoint restorePoint, BackupJob backupJob)
    {
        if (restorePoint is null)
            throw new BackupsException("Restore point cannot be null");
        if (backupJob is null)
            throw new BackupsException("Backup job cannot be null");
        
        backupJob.RemoveRestorePoint(restorePoint);
    }

    public RestorePoint SaveToConcreteRepo(List<Storage> storages, BackupJob backupJob)
    {
        var filePaths = new List<string>();
        var restorePointName = "RestorePoint";
        var newRestorePoint = new RestorePoint("RestorePoint");
        
        storages.ForEach(storage =>
        {
            newRestorePoint.AddFile(storage);
        });

        return newRestorePoint;
    }
}