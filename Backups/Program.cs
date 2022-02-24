
using Backups.Algos;
using Backups.Backup;
using Backups.Repository;

var repository = new Repository("/Users/noname/Desktop/FilesToBackup", "/Users/noname/Desktop/Backups");
var algo = new SplitStoringAlgorithm();
var file1 = new FileData(repository.PathToBackupingFiles, "file1");
var file2 = new FileData(repository.PathToBackupingFiles, "file2");
var file3 = new FileData(repository.PathToBackupingFiles, "file3");
var file4 = new FileData(repository.PathToBackupingFiles, "file4");

var fs = File.Create(file1.FullPath);
fs.Close();
var fs2 = File.Create(file2.FullPath);
fs2.Close();
var fs3 = File.Create(file3.FullPath);
fs3.Close();
var fs4 = File.Create(file4.FullPath);
fs4.Close();

var backupJob = new BackupJob("Job", algo);
backupJob.AddFile(file1);
backupJob.AddFile(file2);
backupJob.AddFile(file3);
backupJob.AddFile(file4);
var restorePoints = new List<RestorePoint>();

// for (var i = 0; i < 1000; i++)
// {
//    restorePoints.Add(backupJob.Execute(repository));
//    if ((i + 1) % 200 == 0)
//    {
//       restorePoints.ForEach(point => repository.DeleteFromRepository(point, backupJob));
//       restorePoints.RemoveRange(0, 200);
//    }
// }
var repositoryMock = new MockRepository("/Users/noname/Desktop/FilesToBackup", "/Users/noname/Desktop/Backups");
var backupJob1 = new BackupJob("Job", algo);
backupJob1.AddFile(file1);
backupJob1.AddFile(file2);
backupJob1.AddFile(file3);
backupJob1.AddFile(file4);

for (var i = 0; i < 1000; i++)
{
    restorePoints.Add(backupJob.Execute(repositoryMock));
    if ((i + 1) % 200 == 0)
    {
        restorePoints.ForEach(point => repositoryMock.DeleteFromRepository(point, backupJob));
        restorePoints.RemoveRange(0, 200);
    }
}