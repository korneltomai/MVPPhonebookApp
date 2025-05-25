using WinformsMVPPhonebookApp.Models;

namespace WinformsMVPPhonebookApp.Views
{
    public partial class MainForm : Form, IMainView
    {
        public List<PhonebookEntry> Entries 
        { 
            get
            {
                var entries = new List<PhonebookEntry>();
                foreach (ListViewItem item in listView.Items)
                {
                    entries.Add(new PhonebookEntry
                    {
                        Name = item.Text,
                        PhoneNumber = item.SubItems[1].Text
                    });
                }
                return entries;
            }
            set
            {
                listView.Items.Clear();
                foreach (var entry in value)
                {
                    var listViewItem = new ListViewItem(entry.Name);
                    listViewItem.SubItems.Add(entry.PhoneNumber);
                    listView.Items.Add(listViewItem);
                }
            }
        }
        public PhonebookEntry? SelectedEntry
        {
            get
            {
                if (listView.SelectedItems.Count == 0)
                    return null;
                else
                {
                    var selectedItem = listView.SelectedItems[0];
                    return new PhonebookEntry
                    {
                        Name = selectedItem.Text,
                        PhoneNumber = selectedItem.SubItems[1].Text
                    };
                }
            }
            set
            {
                if (value != null)
                {
                    foreach (ListViewItem item in listView.Items)
                    {
                        if (item.Text == value.Name && item.SubItems[1].Text == value.PhoneNumber)
                        {
                            item.Selected = true;
                            label1.Text = $"Selected: {value.Name} - {value.PhoneNumber}";
                            break;
                        }
                    }
                }
            }
        }

        public event EventHandler? EntrySelected;

        public MainForm()
        {
            InitializeComponent();

            listView.Columns[1].Width = -2; // Auto-size the second column to fit its content
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }
    }
}
