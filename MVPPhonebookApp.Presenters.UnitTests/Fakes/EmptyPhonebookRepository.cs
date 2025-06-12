using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;


namespace MVPPhonebookApp.Presenters.UnitTests.Fakes
{
    public class EmptyPhonebookRepository : IPhonebookRepository
    {
        public void AddEntry(PhonebookEntry entry) { }

        public void DeleteEntry(PhonebookEntry entry) { }

        public IEnumerable<PhonebookEntry> GetAllEntries() => Enumerable.Empty<PhonebookEntry>();
    }
}
