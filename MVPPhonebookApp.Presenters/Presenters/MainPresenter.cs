using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.Services;
using MVPPhonebookApp.Presenters.Views;

namespace MVPPhonebookApp.Presenters.Presenters;

public class MainPresenter
{
    private readonly IMainView _view;
    private readonly PhonebookEntryService _phonebookEntryService;
    private readonly IAddOrEditDialogService _addOrEditDialogService;

    public MainPresenter(IMainView view, PhonebookEntryService phonebookEntryService, IAddOrEditDialogService addOrEditDialogService)
    {
        _view = view;
        _phonebookEntryService = phonebookEntryService;
        _addOrEditDialogService = addOrEditDialogService;

        _view.DeleteEntryClicked += OnDeleteEntryClicked;
        _view.AddEntryClicked += OnAddEntryClicked;
        _view.UpdateEntryClicked += OnUpdateEntryClicked;

        _phonebookEntryService.EntryAdded += OnEntryAdded;
        _phonebookEntryService.EntryUpdated += OnEntryUpdated;
    }

    public void LoadEntries()
    {
        _view.Entries = _phonebookEntryService.GetAllEntries().ToList();
    }

    private void OnDeleteEntryClicked(object? sender, EventArgs e)
    {
        if (_view.SelectedEntry == null)
        {
            _view.ShowError("No entry selected for deletion.");
            return;
        }

        try
        {
            _phonebookEntryService.DeleteEntry(_view.SelectedEntry);
        }
        catch (Exception ex) when (ex is InvalidOperationException || ex is FileNotFoundException)
        {
            _view.ShowError(ex.Message);
        }
        catch (Exception ex)
        {
            _view.ShowError("An unexpected error occurred: " + ex.Message);
        }

        _view.Entries.Remove(_view.SelectedEntry);
    }

    private void OnAddEntryClicked(object? sender, EventArgs e)
    {
        _addOrEditDialogService.ShowAddOrEditDialog();
    }

    private void OnEntryAdded(object? sender, PhonebookEntry entry)
    {
        _view.Entries.Add(entry);
    }

    private void OnUpdateEntryClicked(object? sender, EventArgs e)
    {
        if (_view.SelectedEntry == null)
        {
            _view.ShowError("No entry selected for editing.");
            return;
        }
        else
            _addOrEditDialogService.ShowAddOrEditDialog(_view.SelectedEntry);
    }

    private void OnEntryUpdated(object? sender, (PhonebookEntry oldEntry, PhonebookEntry newEntry) entries)
    {
        int index = _view.Entries.IndexOf(entries.oldEntry);
        _view.Entries[index] = entries.newEntry;
    }
}
