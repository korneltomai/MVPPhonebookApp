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

        _view.DeleteEntryClicked += DeleteSelectedEntry;
        _view.AddEntryClicked += AddNewEntry;
        _view.UpdateEntryClicked += UpdateEntry;
    }

    public void LoadEntries()
    {
        _view.Entries = _phonebookEntryService.GetAllEntries().ToList();
    }

    private void DeleteSelectedEntry(object? sender, EventArgs e)
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

    private void AddNewEntry(object? sender, EventArgs e)
    {
        _addOrEditDialogService.ShowAddOrEditDialog();
    }

    private void UpdateEntry(object? sender, EventArgs e)
    {
        _addOrEditDialogService.ShowAddOrEditDialog(_view.SelectedEntry);
    }
}
