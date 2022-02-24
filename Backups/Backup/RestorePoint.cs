using Backups.Repository;

namespace Backups.Backup
{
    public class RestorePoint
    {
        private List<Storage> _copiedFilesData;
        private static int _quantity;
        public RestorePoint(string restorePointName)
        {
            if (restorePointName is null)
                throw new BackupsException("Invalid restore point name");
            _copiedFilesData = new List<Storage>();
            Name = restorePointName + _quantity;;
            _quantity++;
        }
        public string Name { get; }

        public IReadOnlyCollection<Storage> CopiedFilesData => _copiedFilesData;

        public void AddFile(Storage storage)
            => _copiedFilesData.Add(storage);
    }
}