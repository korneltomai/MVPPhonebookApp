using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;

namespace MVPPhonebookApp.Presenters.UnitTests.Fakes;

public class FakePhonebookEntryService : PhonebookEntryService
{
    public List<PhonebookEntry> Entries { get; set; } = [];
    public Exception? GetAllEntriesWillThrow { get; set; } = null;
    public bool GetAllEntriesCalled { get; private set; } = false;
    public Exception? AddEntryWillThrow { get; set; } = null;
    public bool AddEntryCalled { get; private set; } = false;
    public Exception? DeleteEntryWillThrow { get; set; } = null;
    public bool DeleteEntryCalled { get; private set; } = false;
    public Exception? UpdateEntryWillThrow { get; set; } = null;
    public bool UpdateEntryCalled { get; private set; } = false;

    public FakePhonebookEntryService() : base(new EmptyPhonebookRepository()) { }

    public override IEnumerable<PhonebookEntry> GetAllEntries()
    {
        GetAllEntriesCalled = true;

        return Entries.AsEnumerable();
    }

    public override void AddEntry(PhonebookEntry entry)
    {
        AddEntryCalled = true;

        if (AddEntryWillThrow != null)
            throw AddEntryWillThrow;
    }

    public override void DeleteEntry(PhonebookEntry entry)
    {
        DeleteEntryCalled = true;

        if (DeleteEntryWillThrow != null)
            throw DeleteEntryWillThrow;
    }

    public override void UpdateEntry(PhonebookEntry oldEntry, PhonebookEntry newEntry)
    {
        UpdateEntryCalled = true;

        if (UpdateEntryWillThrow != null)
            throw UpdateEntryWillThrow;
    }

}
