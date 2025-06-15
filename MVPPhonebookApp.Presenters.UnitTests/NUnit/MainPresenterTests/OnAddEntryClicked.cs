using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.UnitTests.Fakes;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnit.MainPresenterTests;

public class OnAddEntryClicked
{
    [Test]
    public void WhenCalled_ShowsAddOrEditDialog()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var stubEntryService = new FakePhonebookEntryService();
        var mockDialogService = new FakeAddOrEditDialogService();
        var presenter = new MainPresenter(stubMainView, stubEntryService, mockDialogService);

        // Act
        stubMainView.TriggerAddEntryClicked();

        // Assert
        Assert.IsTrue(mockDialogService.ShowAddOrEditDialogCalled);
    }
}
