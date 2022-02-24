using Backups.Backup;
using Backups.Repository;

namespace Backups.Algos
{
    public class SplitStoringAlgorithm : IAlgorithm
    {
        public List<Storage> SaveFiles(List<FileData> filesToSave)
        {
            var storages = new List<Storage>();
            filesToSave.ForEach(file =>
            {
                var storage = new Storage(file.FullPath, $"{file.FileName}.zip");
                storages.Add(storage);
            });

            return storages;
        }
    }
}