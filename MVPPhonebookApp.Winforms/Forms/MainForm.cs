using System.ComponentModel;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Views;

namespace MVPPhonebookApp.Forms
{
    public partial class MainForm : Form, IMainView
    {
        public event EventHandler? DeleteEntryClicked;

        private BindingList<PhonebookEntry> _bindingList = [];

        public IList<PhonebookEntry> Entries
        {
            get => _bindingList;
            set
            {
                _bindingList = new BindingList<PhonebookEntry>(value);
                dataGridView.DataSource = _bindingList;
            }
        }

        public PhonebookEntry? SelectedEntry
        {
            get => dataGridView.CurrentRow?.DataBoundItem as PhonebookEntry;
        }

        public MainForm()
        {
            InitializeComponent();

            dataGridView.DataSource = _bindingList;

            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromKnownColor(System.Drawing.KnownColor.Control);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
