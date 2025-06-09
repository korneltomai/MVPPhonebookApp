using WinformsMVPPhonebookApp.Core.Models;

namespace WinformsMVPPhonebookApp.Core.Repository;

public interface IPhonebookRepository
{
    IEnumerable<PhonebookEntry> GetAllEntries();
    void DeleteEntry(PhonebookEntry entry);
    void AddEntry(PhonebookEntry entry);
}
