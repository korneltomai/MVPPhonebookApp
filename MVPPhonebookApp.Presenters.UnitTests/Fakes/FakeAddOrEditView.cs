using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Views;

namespace MVPPhonebookApp.Presenters.UnitTests.Fakes
{
    public class FakeAddOrEditView : IAddOrEditView
    {
        public event EventHandler? SubmitClicked;
        public PhonebookEntry? Entry { get; set; }
        public string EntryName { get; set; } = string.Empty;
        public string EntryPhoneNumber { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;
        public void ShowError(string message)
        {
            ErrorMessage = message;
        }

        public bool IsClosed { get; private set; } = false;
        public void Close()
        {
            IsClosed = true;
        }

        public void TriggerSubmitClicked()
        {
            SubmitClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
