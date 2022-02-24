using Backups.Backup;
using Backups.Repository;

namespace Backups.Algos
{
    public interface IAlgorithm
    {
        List<Storage> SaveFiles(List<FileData> filesToSave);
    }
}