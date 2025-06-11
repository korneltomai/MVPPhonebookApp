using MVPPhonebookApp.Core.Models;

namespace MVPPhonebookApp.Core.Services;

public interface IPhonebookEntryService
{
    IEnumerable<PhonebookEntry> GetAllEntries();
    void DeleteEntry(PhonebookEntry entry);
    void AddEntry(PhonebookEntry entry);
}
