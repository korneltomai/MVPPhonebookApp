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

        private BindingList<PhonebookEntry> bindingList = [];

        public MainPresenter(IMainView view, IPhonebookRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.DeleteEntryClicked += DeleteSelectedEntry;

            LoadEntries();
        }

        private void LoadEntries()
        {
            bindingList = new(_repository.GetAllEntries().ToList());
            _view.Entries = bindingList;
        }

        private void DeleteSelectedEntry(object? sender, EventArgs e)
        {
            if (_view.SelectedEntry != null)
            {
                _repository.DeleteEntry(_view.SelectedEntry);
                bindingList.Remove(_view.SelectedEntry);
            }
        }
    }
}
