using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Services;

namespace MVPPhonebookApp.Presenters.UnitTests.Fakes
{
    public class FakeAddOrEditDialogService : IAddOrEditDialogService
    {
        public bool DialogShown { get; private set; } = false;
        PhonebookEntry? Entry { get; set; } = null;
        public void ShowAddOrEditDialog(PhonebookEntry? entry = null)
        {
            DialogShown = true;
            Entry = entry;
        }
    }
}
