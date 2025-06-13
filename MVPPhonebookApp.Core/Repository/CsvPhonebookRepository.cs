using MVPPhonebookApp.Core.FileSystem;
using MVPPhonebookApp.Core.Models;

namespace MVPPhonebookApp.Core.Repository;

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

            List<string> lines = reader.ReadToEnd().Split("\r\n").ToList();

            if (lines.Count > 1 && lines.Any(l => l.Trim() == string.Empty && l != lines.Last()))
                throw new InvalidOperationException("The entries file must not contain empty lines.");

            foreach (string line in lines)
            {
                if (line == lines.Last() && line == string.Empty)
                    break;

                string[] values = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                if (values.Length != 2)
                    throw new InvalidOperationException("Each line in the entries file must contain exactly two values.");

                entries.Add(new PhonebookEntry(values[0], values[1]));
            }
        }
        else
        {
            using var stream = _fileSystem.CreateFile(_filePath);
        }

        return entries;
    }

    public void DeleteEntry(PhonebookEntry entry)
    {
        if (!_fileSystem.FileExists(_filePath))
            throw new FileNotFoundException("The entries file does not exist.");

        List<PhonebookEntry> entries = GetAllEntries().ToList();

        entries.RemoveAll(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber);

        using var stream = _fileSystem.CreateFile(_filePath);
        using var writer = new StreamWriter(stream, leaveOpen: true);

        foreach (var e in entries)
        {
            writer.WriteLine($"{e.Name},{e.PhoneNumber}");
        }
    }

    public void AddEntry(PhonebookEntry entry)
    {
        if (!_fileSystem.FileExists(_filePath))
           throw new FileNotFoundException("The entries file does not exist.");

        List<PhonebookEntry> entries = GetAllEntries().ToList();

        entries.Add(entry);

        using var stream = _fileSystem.CreateFile(_filePath);
        using var writer = new StreamWriter(stream, leaveOpen: true);

        foreach (var e in entries)
        {
            writer.WriteLine($"{e.Name},{e.PhoneNumber}");
        }
    }

    public void UpdateEntry(PhonebookEntry oldEntry, PhonebookEntry newEntry)
    {
        if (!_fileSystem.FileExists(_filePath))
            throw new FileNotFoundException("The entries file does not exist.");

        List<PhonebookEntry> entries = GetAllEntries().ToList();

        int index = entries.FindIndex(e => e.Name == oldEntry.Name && e.PhoneNumber == oldEntry.PhoneNumber);
        entries[index] = newEntry;

        using var stream = _fileSystem.CreateFile(_filePath);
        using var writer = new StreamWriter(stream, leaveOpen: true);

        foreach (var e in entries)
        {
            writer.WriteLine($"{e.Name},{e.PhoneNumber}");
        }
    }

    public bool EntryExists(PhonebookEntry entry) =>
        GetAllEntries().Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber);

    public bool EntryExistsByName(string name) =>
        GetAllEntries().Any(e => e.Name == name);

    public bool EntryExistsByPhoneNumber(string phoneNumber) =>
        GetAllEntries().Any(e => e.PhoneNumber == phoneNumber);
}
