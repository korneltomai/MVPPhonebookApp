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
        if (!_repository.EntryExists(entry))
            throw new InvalidOperationException("The entry to delete does not exist.");
        _repository.DeleteEntry(entry);
    }

    public virtual void AddEntry(PhonebookEntry entry)
    {
        if (_repository.EntryExists(entry))
            throw new InvalidOperationException("An entry with the same values already exists.");
        if (_repository.EntryExistsByName(entry.Name))
            throw new InvalidOperationException("An entry with the same name already exists.");
        if (_repository.EntryExistsByPhoneNumber(entry.PhoneNumber))
            throw new InvalidOperationException("An entry with the same phone number already exists.");

        _repository.AddEntry(entry);
        EntryAdded?.Invoke(this, entry);
    }

    public virtual void UpdateEntry(PhonebookEntry oldEntry, PhonebookEntry newEntry)
    {
        if (!_repository.EntryExists(oldEntry))
            throw new InvalidOperationException("The entry to update does not exist.");
        if (newEntry.Name != oldEntry.Name && newEntry.PhoneNumber != oldEntry.PhoneNumber && _repository.EntryExists(newEntry))
            throw new InvalidOperationException("An entry with the same values already exists.");
        if (newEntry.Name != oldEntry.Name && _repository.EntryExistsByName(newEntry.Name))
            throw new InvalidOperationException("An entry with the same name already exists.");
        if (newEntry.PhoneNumber != oldEntry.PhoneNumber && _repository.EntryExistsByPhoneNumber(newEntry.PhoneNumber))
            throw new InvalidOperationException("An entry with the same phone number already exists.");

        _repository.UpdateEntry(oldEntry, newEntry);
        EntryUpdated?.Invoke(this, (oldEntry, newEntry));
    }

}

