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

            LoadEntries();
        }

        private void LoadEntries()
        {
            _view.Entries = _repository.GetAllEntries().ToList();
        }
    }
}
