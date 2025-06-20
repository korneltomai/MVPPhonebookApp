﻿using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;

namespace MVPPhonebookApp.Core.Services;

public class PhonebookEntryService
{
    private readonly IPhonebookRepository _repository;

    public virtual event EventHandler<PhonebookEntry>? EntryAdded;
    public virtual event EventHandler<(PhonebookEntry OldEntry, PhonebookEntry NewEntry)>? EntryUpdated;

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
        ValidateEntry(entry);

        if (_repository.EntryExists(entry))
            throw new ValidationException("An entry with the same values already exists.");
        if (_repository.EntryExistsByName(entry.Name))
            throw new ValidationException("An entry with the same name already exists.");
        if (_repository.EntryExistsByPhoneNumber(entry.PhoneNumber))
            throw new ValidationException("An entry with the same phone number already exists.");

        _repository.AddEntry(entry);
        OnEntryAdded(entry);
    }

    public virtual void UpdateEntry(PhonebookEntry oldEntry, PhonebookEntry newEntry)
    {
        ValidateEntry(newEntry);

        if (!_repository.EntryExists(oldEntry))
            throw new InvalidOperationException("The entry to update does not exist.");

        if (newEntry.Name != oldEntry.Name && newEntry.PhoneNumber != oldEntry.PhoneNumber && _repository.EntryExists(newEntry))
            throw new ValidationException("An entry with the same values already exists.");
        if (newEntry.Name != oldEntry.Name && _repository.EntryExistsByName(newEntry.Name))
            throw new ValidationException("An entry with the same name already exists.");
        if (newEntry.PhoneNumber != oldEntry.PhoneNumber && _repository.EntryExistsByPhoneNumber(newEntry.PhoneNumber))
            throw new ValidationException("An entry with the same phone number already exists.");

        _repository.UpdateEntry(oldEntry, newEntry);
        OnEntryUpdated(oldEntry, newEntry);
    }

    public virtual void OnEntryAdded(PhonebookEntry entry)
    {
        EntryAdded?.Invoke(this, entry);
    }

    public virtual void OnEntryUpdated(PhonebookEntry oldEntry, PhonebookEntry newEntry)
    {
        EntryUpdated?.Invoke(this, (oldEntry, newEntry));
    }

    private void ValidateEntry(PhonebookEntry entry)
    {
        if (string.IsNullOrWhiteSpace(entry.Name) || string.IsNullOrWhiteSpace(entry.PhoneNumber))
            throw new ValidationException("Name and phone number cannot be empty.");
        if (entry.Name.Length > 32)
            throw new ValidationException("Name cannot exceed 32 characters.");
        if (entry.PhoneNumber.Length > 16)
            throw new ValidationException("Phone number cannot exceed 16 characters.");
    }


}

