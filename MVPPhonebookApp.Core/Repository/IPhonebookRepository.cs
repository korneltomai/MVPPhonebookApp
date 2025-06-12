using MVPPhonebookApp.Core.Models;

namespace MVPPhonebookApp.Core.Repository;

public interface IPhonebookRepository
{
    IEnumerable<PhonebookEntry> GetAllEntries();
    void DeleteEntry(PhonebookEntry entry);
    void AddEntry(PhonebookEntry entry);
    void UpdateEntry(PhonebookEntry oldEntry, PhonebookEntry newEntry);
}
