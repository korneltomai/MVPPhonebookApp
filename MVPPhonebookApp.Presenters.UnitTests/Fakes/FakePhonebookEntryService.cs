using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;

namespace MVPPhonebookApp.Presenters.UnitTests.Fakes;

public class FakePhonebookEntryService : IPhonebookEntryService
{
    public List<PhonebookEntry> Entries { get; set; } = [];
    public Exception? AddEntryWillThrow { get; set; } = null;
    public bool AddEntryCalled { get; private set; } = false;
    public Exception? DeleteEntryWillThrow { get; set; } = null;
    public bool DeleteEntryCalled { get; private set; } = false;
    public bool GetAllEntriesCalled { get; private set; } = false;

    public FakePhonebookEntryService() : base() { }

public void AddEntry(PhonebookEntry entry)
    {
        AddEntryCalled = true;

        if (AddEntryWillThrow != null)
            throw AddEntryWillThrow;
    }

    public void DeleteEntry(PhonebookEntry entry)
    {
        DeleteEntryCalled = true;

        if (DeleteEntryWillThrow != null)
            throw DeleteEntryWillThrow;
    }

    public IEnumerable<PhonebookEntry> GetAllEntries()
    {
        GetAllEntriesCalled = true;

        return Entries.AsEnumerable();
    }
}
