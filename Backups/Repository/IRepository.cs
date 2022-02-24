using Backups.Backup;

namespace Backups.Repository
{
    public interface IRepository
    {
        void DeleteFromRepository(RestorePoint restorePoint, BackupJob backupJob);
        RestorePoint SaveToConcreteRepo(List<Storage> zips, BackupJob backupJob);
    }
}