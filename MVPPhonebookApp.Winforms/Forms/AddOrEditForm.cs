using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Views;

namespace MVPPhonebookApp.Winforms.Forms
{
    public partial class AddOrEditForm : Form, IAddOrEditView
    {
        public event EventHandler? SubmitClicked;

        public PhonebookEntry? Entry { get; set; }
        public string EntryName { get => textBoxName.Text; set => textBoxName.Text = value; }
        public string EntryPhoneNumber { get => textBoxPhoneNumber.Text; set => textBoxPhoneNumber.Text = value; }

        public AddOrEditForm(PhonebookEntry? entry)
        {
            InitializeComponent();

            buttonSubmit.Click += (sender, e) => SubmitClicked?.Invoke(this, EventArgs.Empty);
            buttonCancel.Click += (sender, e) => Close();

            Entry = entry;
            if (entry != null)
            {
                EntryName = entry.Name;
                EntryPhoneNumber = entry.PhoneNumber;
            }
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
