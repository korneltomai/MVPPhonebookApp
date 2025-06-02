using System.Windows.Forms;
using WinformsMVPPhonebookApp.Models;

namespace WinformsMVPPhonebookApp.Views
{
    public partial class MainForm : Form, IMainView
    {
        public List<PhonebookEntry> Entries
        {
            get => dataGridView.DataSource as List<PhonebookEntry> ?? [];
            set => dataGridView.DataSource = value;
        }

        public PhonebookEntry? SelectedEntry
        {
            get => dataGridView.CurrentRow.DataBoundItem as PhonebookEntry;
        }

        public MainForm()
        {
            InitializeComponent();

            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromKnownColor(System.Drawing.KnownColor.Control);
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            buttonEditPhonenumber.Enabled = SelectedEntry != null;
            buttonDeletePhonenumber.Enabled = SelectedEntry != null;
        }
    }
}
