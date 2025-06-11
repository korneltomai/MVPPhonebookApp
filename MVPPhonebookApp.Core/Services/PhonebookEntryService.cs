using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;

namespace MVPPhonebookApp.Core.Services;

public class PhonebookEntryService : IPhonebookEntryService
{
    private readonly IPhonebookRepository? _repository;

    public PhonebookEntryService(IPhonebookRepository repository)
    {
        _repository = repository;
    }

    public PhonebookEntryService() { }

    public IEnumerable<PhonebookEntry> GetAllEntries()
    {
        return _repository!.GetAllEntries();
    }
    public void DeleteEntry(PhonebookEntry entry)
    {
        _repository!.DeleteEntry(entry);
    }
    public void AddEntry(PhonebookEntry entry)
    {
        _repository!.AddEntry(entry);
    }
}