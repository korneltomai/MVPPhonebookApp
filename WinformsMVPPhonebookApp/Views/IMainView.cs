using WinformsMVPPhonebookApp.Models;

namespace WinformsMVPPhonebookApp.Views
{
    public interface IMainView
    {
        List<PhonebookEntry> Entries { get; set; }
        PhonebookEntry? SelectedEntry { get; }
    }
}
