using WinformsMVPPhonebookApp.Models;
using WinformsMVPPhonebookApp.Views;

namespace WinformsMVPPhonebookApp.UnitTests.Models.Fakes
{
    public class FakeMainView : IMainView
    {
        public object Entries { get; set; } = new List<PhonebookEntry>();

        public PhonebookEntry? SelectedEntry { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public event EventHandler? DeleteEntryClicked;

        public void ShowError(string message)
        {
            ErrorMessage = message;
        }
        public void TriggerDeleteEntryClicked()
        {
            DeleteEntryClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
