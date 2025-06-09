using WinformsMVPPhonebookApp.Models;

namespace WinformsMVPPhonebookApp.Views
{
    public interface IMainView
    {
        event EventHandler? DeleteEntryClicked;
        IList<PhonebookEntry> Entries { get; }
        PhonebookEntry? SelectedEntry { get; }
        void ShowError(string message);
    }
}
