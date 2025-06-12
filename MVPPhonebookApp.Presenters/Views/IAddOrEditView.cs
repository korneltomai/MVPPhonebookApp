using MVPPhonebookApp.Core.Models;

namespace MVPPhonebookApp.Presenters.Views
{
    public interface IAddOrEditView
    {
        event EventHandler? SubmitClicked;
        PhonebookEntry? Entry { get; set; }
        string EntryName { get; set; }
        string EntryPhoneNumber { get; set; }
        void ShowError(string message);
        void Close();
    }
}
