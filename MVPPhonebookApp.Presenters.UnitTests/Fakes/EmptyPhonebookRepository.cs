using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;


namespace MVPPhonebookApp.Presenters.UnitTests.Fakes
{
    public class EmptyPhonebookRepository : IPhonebookRepository
    {
        public IEnumerable<PhonebookEntry> GetAllEntries() => [];

        public void AddEntry(PhonebookEntry entry) { }

        public void DeleteEntry(PhonebookEntry entry) { }

        public void UpdateEntry(PhonebookEntry oldEntry, PhonebookEntry newEntry) { }
        public bool EntryExists(PhonebookEntry entry) => false;
        public bool EntryExistsByName(string name) => false;
        public bool EntryExistsByPhoneNumber(string phoneNumber) => false;
    }
}
