namespace WinformsMVPPhonebookApp.Models
{
    public interface IPhonebookRepository
    {
        IEnumerable<PhonebookEntry> GetAllEntries();
        void DeleteEntry(PhonebookEntry entry);
        void AddEntry(PhonebookEntry entry);
    }
}
