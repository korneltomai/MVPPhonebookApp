using WinformsMVPPhonebookApp.Models;

namespace WinformsMVPPhonebookApp.Views
{
    public interface IMainView
    {
        event EventHandler DeleteEntryClicked;

        object Entries { get; set; }
        PhonebookEntry? SelectedEntry { get; }
    }
}
