namespace Backups.Repository
{
    public class Storage
    {
        public Storage(string pathToFileToSaveFrom, string pathToFileToSaveTo)
        {
            PathToFileToSave = pathToFileToSaveFrom;
            ZipName = pathToFileToSaveTo;
        }

        public string PathToFileToSave { get; }
        public string ZipName { get; }
    }
}