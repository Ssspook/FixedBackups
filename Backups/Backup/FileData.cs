namespace Backups.Backup
{
    public class FileData
    {
        public FileData(string directoryPath, string filename)
        {
            FileName = filename;
            DirectoryPath = directoryPath;
            FullPath = $"{DirectoryPath}/{FileName}";
        }

        public string FileName { get; }
        public string DirectoryPath { get; }
        public string FullPath { get; }
    }
}