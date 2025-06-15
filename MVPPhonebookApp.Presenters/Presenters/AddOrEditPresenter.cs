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

            _view.SubmitClicked += OnSubmitClicked;
        }

        private void OnSubmitClicked(object? sender, EventArgs e)
        {
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
            catch (Exception ex) when (ex is ValidationException)
            {
                _view.ShowError(ex.Message);
                return;
            }
            catch (Exception ex)
            {
                _view.ShowError("An unexpected error occurred: " + ex.Message);
            }

            _view.Close();
        }
    }
}
