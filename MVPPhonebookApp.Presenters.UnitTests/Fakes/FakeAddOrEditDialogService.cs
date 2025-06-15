using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Services;

namespace MVPPhonebookApp.Presenters.UnitTests.Fakes
{
    public class FakeAddOrEditDialogService : IAddOrEditDialogService
    {
        public bool ShowAddOrEditDialogCalled { get; private set; } = false;
        public PhonebookEntry? Entry { get; set; } = null;
        public void ShowAddOrEditDialog(PhonebookEntry? entry = null)
        {
            ShowAddOrEditDialogCalled = true;
            Entry = entry;
        }
    }
}
