namespace WinformsMVPPhonebookApp.Models
{
    public class CsvPhonebookRepository : IPhonebookRepository
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _filePath;

        public CsvPhonebookRepository(IFileSystem fileSystem, string filePath)
        {
            _fileSystem = fileSystem;
            _filePath = filePath;
        }

        public IEnumerable<PhonebookEntry> GetAllEntries()
        {
            List<PhonebookEntry> entries = [];

            if (_fileSystem.FileExists(_filePath))
            {
                using var stream = _fileSystem.OpenRead(_filePath);
                using var reader = new StreamReader(stream);

                List<string> lines = reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (string line in lines)
                {
                    string[] values = line.Split(',');
                    entries.Add(new PhonebookEntry(values[0], values[1]));
                }
            }
            else
            {
                using var stream = _fileSystem.CreateFile(_filePath);
            }

            return entries;
        }
    }
}
