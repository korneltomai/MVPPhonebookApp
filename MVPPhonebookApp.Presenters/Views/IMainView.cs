using MVPPhonebookApp.Core.Models;

namespace MVPPhonebookApp.Presenters.Views;

public interface IMainView
{
    event EventHandler DeleteEntryClicked;
    event EventHandler AddEntryClicked;
    event EventHandler UpdateEntryClicked;

    IList<PhonebookEntry> Entries { get; set; }
    PhonebookEntry? SelectedEntry { get; }
    void ShowError(string message);
}
