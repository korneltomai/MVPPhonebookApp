using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.Views;

namespace MVPPhonebookApp.Presenters.Presenters
{
    public class AddOrEditPresenter
    {
        private readonly IAddOrEditView _view;
        private readonly PhonebookEntryService _phonebookEntryService;

        public AddOrEditPresenter(IAddOrEditView view, PhonebookEntryService phonebookEntryService)
        {
            _view = view;
            _phonebookEntryService = phonebookEntryService;

            _view.SubmitClicked += Submit;
        }

        private void Submit(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_view.EntryName) || string.IsNullOrWhiteSpace(_view.EntryPhoneNumber))
            {
                _view.ShowError("Name and phone number cannot be empty.");
                return;
            }

            var newEntry = new PhonebookEntry(_view.EntryName, _view.EntryPhoneNumber);

            try
            {
                if (_view.Entry == null)
                {
                    _phonebookEntryService.AddEntry(newEntry);
                }
                else
                {
                    _phonebookEntryService.UpdateEntry(_view.Entry, newEntry);
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is FileNotFoundException)
            {
                _view.ShowError(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                _view.ShowError("An unexpected error occurred: " + ex.Message);
                return;
            }

            _view.Close();
        }
    }
}
