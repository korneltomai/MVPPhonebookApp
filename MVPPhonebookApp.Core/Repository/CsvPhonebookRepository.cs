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

        if (!entries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber))
            throw new InvalidOperationException("The entry to delete does not exist.");

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

        if (entries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber))
            throw new InvalidOperationException("An entry with the same values already exists.");
        else if (entries.Any(e => e.Name == entry.Name))
            throw new InvalidOperationException("An entry with the same name already exists.");
        else if (entries.Any(e => e.PhoneNumber == entry.PhoneNumber))
            throw new InvalidOperationException("An entry with the same phone number already exists.");

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

    }
}
