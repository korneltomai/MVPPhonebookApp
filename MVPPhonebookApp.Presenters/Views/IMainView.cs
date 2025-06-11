using MVPPhonebookApp.Core.Models;

namespace MVPPhonebookApp.Presenters.Views;

public interface IMainView
{
    event EventHandler? DeleteEntryClicked;
    IList<PhonebookEntry> Entries { get; set; }
    PhonebookEntry? SelectedEntry { get; }
    void ShowError(string message);
}
