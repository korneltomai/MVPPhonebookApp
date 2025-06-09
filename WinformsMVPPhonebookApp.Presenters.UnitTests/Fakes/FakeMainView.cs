using WinformsMVPPhonebookApp.Core.Models;
using WinformsMVPPhonebookApp.Presenters.Views;

namespace WinformsMVPPhonebookApp.Presenters.UnitTests.Fakes;

public class FakeMainView : IMainView
{
    public IList<PhonebookEntry> Entries { get; set; } = [];

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
