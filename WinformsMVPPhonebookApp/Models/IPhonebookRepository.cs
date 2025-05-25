namespace WinformsMVPPhonebookApp.Models
{
    public interface IPhonebookRepository
    {
        IEnumerable<PhonebookEntry> GetAllEntries();
    }
}
