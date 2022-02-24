using Backups.Algos;
using Backups.Repository;

namespace Backups.Backup
{
    public class BackupJob
    {
        private List<RestorePoint> _restorePoints;
        private IAlgorithm _algorithm;
        private List<FileData> _backupingFiles;

        public BackupJob(string name, IAlgorithm algorithm)
        {
            if (name is null)
                throw new BackupsException("Invalid name data");
            if (algorithm is null)
                throw new BackupsException("Invalid algorithm data");
            _restorePoints = new List<RestorePoint>();
            _backupingFiles = new List<FileData>();

            Name = name;
            _algorithm = algorithm;
        }
        public string Name { get; }
        public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;
        public RestorePoint Execute(IRepository repository)
        {
            var zips = _algorithm.SaveFiles(_backupingFiles);
            var newRestorePoint = repository.SaveToConcreteRepo(zips, this);
            _restorePoints.Add(newRestorePoint);

            return newRestorePoint;
        }
        public void DeleteFile(FileData file)
        {
            if (file is null)
                throw new BackupsException("Invalid file");
            _backupingFiles.Remove(file);
        }

        public void AddFile(FileData file)
        {
            if (file is null)
                throw new BackupsException("Invalid file");
            _backupingFiles.Add(file);
        }
        public void RemoveRestorePoint(RestorePoint restorePoint)
        {
            if (!_restorePoints.Contains(restorePoint))
                throw new BackupsException("Restore point is not in this backup job");
                    
            _restorePoints.Remove(restorePoint);
        }
    }
}