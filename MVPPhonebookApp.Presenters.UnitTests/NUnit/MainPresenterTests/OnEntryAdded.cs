using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.UnitTests.Fakes;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnit.MainPresenterTests;

public class OnEntryAdded
{
    [Test]
    public void WhenCalled_AddsEntryToView()
    {
        // Arrange
        var mockMainView = new FakeMainView();
        var stubEntryService = new FakePhonebookEntryService();
        var stubDialogService = new FakeAddOrEditDialogService();
        var presenter = new MainPresenter(mockMainView, stubEntryService, stubDialogService);

        var entryToAdd = new PhonebookEntry("John Doe", "123456789");

        // Act
        stubEntryService.OnEntryAdded(entryToAdd);

        // Assert
        Assert.IsTrue(mockMainView.Entries.Contains(entryToAdd));
    }
}
