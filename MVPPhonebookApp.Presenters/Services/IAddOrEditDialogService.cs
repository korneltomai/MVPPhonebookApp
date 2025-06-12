using MVPPhonebookApp.Core.Models;

namespace MVPPhonebookApp.Presenters.Services
{
    public interface IAddOrEditDialogService
    {
        public void ShowAddOrEditDialog(PhonebookEntry? entry = null);
    }
}
