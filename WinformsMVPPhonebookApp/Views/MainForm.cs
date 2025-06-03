using System.Windows.Forms;
using WinformsMVPPhonebookApp.Models;

namespace WinformsMVPPhonebookApp.Views
{
    public partial class MainForm : Form, IMainView
    {
        public event EventHandler? DeleteEntryClicked;

        public object Entries
        {
            get => dataGridView.DataSource;
            set => dataGridView.DataSource = value;
        }

        public PhonebookEntry? SelectedEntry
        {
            get => dataGridView.CurrentRow?.DataBoundItem as PhonebookEntry;
        }

        public MainForm()
        {
            InitializeComponent();

            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromKnownColor(System.Drawing.KnownColor.Control);
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            buttonEditEntry.Enabled = SelectedEntry != null;
            buttonDeleteEntry.Enabled = SelectedEntry != null;
        }

        private void ButtonDeleteEntry_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected entry?",
                "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
                DeleteEntryClicked?.Invoke(sender, e);
        }
    }
}
