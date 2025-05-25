using WinformsMVPPhonebookApp.Models;

namespace WinformsMVPPhonebookApp.Views
{
    public interface IMainView
    {
        public List<PhonebookEntry> Entries { get; set; }
        public PhonebookEntry? SelectedEntry { get; set; }

        event EventHandler? EntrySelected;
    }
}
