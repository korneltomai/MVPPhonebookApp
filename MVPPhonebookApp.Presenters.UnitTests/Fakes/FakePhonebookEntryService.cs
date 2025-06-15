using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;

namespace MVPPhonebookApp.Presenters.UnitTests.Fakes;

public class FakePhonebookEntryService : PhonebookEntryService
{
    public IEnumerable<PhonebookEntry> Entries { get; set; } = [];
    public Exception? GetAllEntriesWillThrow { get; set; } = null;
    public bool GetAllEntriesCalled { get; private set; } = false;
    public Exception? AddEntryWillThrow { get; set; } = null;
    public bool AddEntryCalled { get; private set; } = false;
    public PhonebookEntry? AddEntryParameter { get; private set; }
    public Exception? DeleteEntryWillThrow { get; set; } = null;
    public bool DeleteEntryCalled { get; private set; } = false;
    public PhonebookEntry? DeleteEntryParameter { get; private set; }
    public Exception? UpdateEntryWillThrow { get; set; } = null;
    public bool UpdateEntryCalled { get; private set; } = false;
    public (PhonebookEntry? oldEntry, PhonebookEntry? newEntry) UpdateEntryParameters { get; private set; }

    public FakePhonebookEntryService() : base(new EmptyPhonebookRepository()) { }

    public override IEnumerable<PhonebookEntry> GetAllEntries()
    {
        GetAllEntriesCalled = true;

        return Entries;
    }

    public override void AddEntry(PhonebookEntry entry)
    {
        AddEntryCalled = true;

        if (AddEntryWillThrow != null)
            throw AddEntryWillThrow;

        AddEntryParameter = entry;
    }

    public override void DeleteEntry(PhonebookEntry entry)
    {
        DeleteEntryCalled = true;

        if (DeleteEntryWillThrow != null)
            throw DeleteEntryWillThrow;

        DeleteEntryParameter = entry;
    }

    public override void UpdateEntry(PhonebookEntry oldEntry, PhonebookEntry newEntry)
    {
        UpdateEntryCalled = true;

        if (UpdateEntryWillThrow != null)
            throw UpdateEntryWillThrow;

        UpdateEntryParameters = (oldEntry, newEntry);
    }

    public override void OnEntryAdded(PhonebookEntry entry)
    {
        base.OnEntryAdded(entry);
    }

    public override void OnEntryUpdated(PhonebookEntry oldEntry, PhonebookEntry newEntry)
    {
        base.OnEntryUpdated(oldEntry, newEntry);
    }
}
