using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.UnitTests.Fakes;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnit.MainPresenterTests;

public class OnUpdateEntryClicked
{
    [Test]
    public void WhenSelectedEntryIsNotNull_ShowsAddOrEditDialog()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var stubEntryService = new FakePhonebookEntryService();
        var mockDialogService = new FakeAddOrEditDialogService();
        var mainPresenter = new MainPresenter(stubMainView, stubEntryService, mockDialogService);

        stubMainView.SelectedEntry = new PhonebookEntry("John Doe", "123456789");

        // Act  
        stubMainView.TriggerUpdateEntryClicked();

        // Assert
        Assert.That(mockDialogService.ShowAddOrEditDialogCalled, Is.True);
        Assert.That(mockDialogService.Entry, Is.EqualTo(stubMainView.SelectedEntry));
    }

    [Test]
    public void WhenSelectedEntryIsNull_SendsErrorToView()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var stubEntryService = new FakePhonebookEntryService();
        var mockDialogService = new FakeAddOrEditDialogService();
        var mainPresenter = new MainPresenter(stubMainView, stubEntryService, mockDialogService);

        stubMainView.SelectedEntry = null;

        // Act  
        stubMainView.TriggerUpdateEntryClicked();

        // Assert
        Assert.That(stubMainView.ErrorMessage, Contains.Substring("No entry selected for editing."));
    }
}
