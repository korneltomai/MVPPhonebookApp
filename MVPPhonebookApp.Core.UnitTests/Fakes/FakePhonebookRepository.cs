using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;

namespace MVPPhonebookApp.Core.UnitTests.Fakes
{
    internal class FakePhonebookRepository : IPhonebookRepository
    {
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

        public IEnumerable<PhonebookEntry> GetAllEntries()
        {
            GetAllEntriesCalled = true;

            return [];
        }

        public void AddEntry(PhonebookEntry entry)
        {
            AddEntryCalled = true;

            if (AddEntryWillThrow != null)
                throw AddEntryWillThrow;

            AddEntryParameter = entry;
        }

        public void DeleteEntry(PhonebookEntry entry)
        {
            DeleteEntryCalled = true;

            if (DeleteEntryWillThrow != null)
                throw DeleteEntryWillThrow;

            DeleteEntryParameter = entry;
        }

        public void UpdateEntry(PhonebookEntry oldEntry, PhonebookEntry newEntry)
        {
            UpdateEntryCalled = true;

            if (UpdateEntryWillThrow != null)
                throw UpdateEntryWillThrow;

            UpdateEntryParameters = (oldEntry, newEntry);
        }
    }
}
