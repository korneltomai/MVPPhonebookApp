using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;

namespace MVPPhonebookApp.Core.Services;

public class PhonebookEntryService
{
    private readonly IPhonebookRepository _repository;

    public event EventHandler<PhonebookEntry>? EntryAdded;
    public event EventHandler<(PhonebookEntry OldEntry, PhonebookEntry NewEntry)>? EntryUpdated;

    public PhonebookEntryService(IPhonebookRepository repository)
    {
        _repository = repository;
    }

    public virtual IEnumerable<PhonebookEntry> GetAllEntries()
    {
        return _repository!.GetAllEntries();
    }

    public virtual void DeleteEntry(PhonebookEntry entry)
    {
        _repository!.DeleteEntry(entry);
    }

    public virtual void AddEntry(PhonebookEntry entry)
    {
        _repository!.AddEntry(entry);
        EntryAdded?.Invoke(this, entry);
    }

    public virtual void UpdateEntry(PhonebookEntry oldEntry, PhonebookEntry newEntry)
    {
        _repository!.UpdateEntry(oldEntry, newEntry);
        EntryUpdated?.Invoke(this, (oldEntry, newEntry));
    }

}

