using System.ComponentModel;
using System.Data;
using WinformsMVPPhonebookApp.Models;
using WinformsMVPPhonebookApp.Views;

namespace WinformsMVPPhonebookApp.Presenters
{
    public class MainPresenter
    {
        private readonly IMainView _view;
        private readonly IPhonebookRepository _repository;

        public BindingList<PhonebookEntry> BindingList { get; private set; } = [];

        public MainPresenter(IMainView view, IPhonebookRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.DeleteEntryClicked += DeleteSelectedEntry;
        }

        public void LoadEntries()
        {
            BindingList = new(_repository.GetAllEntries().ToList());
            _view.Entries = BindingList;
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

            BindingList.Remove(_view.SelectedEntry);
        }
    }
}
