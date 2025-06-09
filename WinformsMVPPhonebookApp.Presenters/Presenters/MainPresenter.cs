using WinformsMVPPhonebookApp.Core.Repository;
using WinformsMVPPhonebookApp.Presenters.Views;

namespace WinformsMVPPhonebookApp.Presenters.Presenters;

public class MainPresenter
{
    private readonly IMainView _view;
    private readonly IPhonebookRepository _repository;

    public MainPresenter(IMainView view, IPhonebookRepository repository)
    {
        _view = view;
        _repository = repository;

        _view.DeleteEntryClicked += DeleteSelectedEntry;
    }

    public void LoadEntries()
    {
        _view.Entries = _repository.GetAllEntries().ToList();
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
            _repository.DeleteEntry(_view.SelectedEntry);
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
}
