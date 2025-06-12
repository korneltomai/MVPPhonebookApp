using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;


namespace MVPPhonebookApp.Presenters.UnitTests.Fakes
{
    public class EmptyPhonebookRepository : IPhonebookRepository
    {
        public IEnumerable<PhonebookEntry> GetAllEntries() => Enumerable.Empty<PhonebookEntry>();

        public void AddEntry(PhonebookEntry entry) { }

        public void DeleteEntry(PhonebookEntry entry) { }

        public void UpdateEntry(PhonebookEntry oldEntry, PhonebookEntry newEntry) { }
    }
}
