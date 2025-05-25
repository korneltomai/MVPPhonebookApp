using WinformsMVPPhonebookApp.Models;
using WinformsMVPPhonebookApp.Views;

namespace WinformsMVPPhonebookApp.Presenters
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly IPhonebookRepository _repository;

        public MainPresenter(IMainView view, IPhonebookRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.EntrySelected += OnEntrySelected;

            LoadEntries();
        }

        private void OnEntrySelected(object? sender, EventArgs e)
        {
            _view.SelectedEntry = _view.SelectedEntry;
        }

        private void LoadEntries()
        {
            _view.Entries = _repository.GetAllEntries().ToList();
        }
    }
}
