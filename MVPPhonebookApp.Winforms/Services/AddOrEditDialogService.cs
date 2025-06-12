using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.Services;
using MVPPhonebookApp.Winforms.Forms;

namespace MVPPhonebookApp.Winforms.Services;

public class AddOrEditDialogService : IAddOrEditDialogService
{
    private readonly PhonebookEntryService _phonebookEntryService;

    public AddOrEditDialogService(PhonebookEntryService phonebookEntryService)
    {
        _phonebookEntryService = phonebookEntryService;
    }

    public void ShowAddOrEditDialog(PhonebookEntry? entry = null)
    {
        var view = new AddOrEditForm(entry);
        var presenter = new AddOrEditPresenter(view, _phonebookEntryService);
        view.ShowDialog();
    }
}
