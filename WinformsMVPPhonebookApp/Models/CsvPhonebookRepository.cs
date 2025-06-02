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

                List<string> lines = reader.ReadToEnd().Split('\n').ToList();

                if (lines.Count > 1 && lines.Any(l => l.Trim() == string.Empty))
                    throw new InvalidOperationException("The CSV file contains empty lines.");

                foreach (string line in lines)
                {
                    string[] values = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    if (values.Length != 2)
                        throw new InvalidOperationException("Each line in the CSV file must contain exactly two values.");

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
