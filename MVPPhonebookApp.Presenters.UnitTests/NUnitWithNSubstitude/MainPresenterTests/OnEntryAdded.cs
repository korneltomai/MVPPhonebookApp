using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.Views;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.Services;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnitWithNSubstitude.MainPresenterTests;

public class OnEntryAdded
{
    [Test]
    public void WhenCalled_AddsEntryToView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        mockMainView.Entries.Returns(new List<PhonebookEntry>());
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var stubEntryService = Substitute.For<PhonebookEntryService>(stubRepository);
        var stubDialogService = Substitute.For<IAddOrEditDialogService>();
        var presenter = new MainPresenter(mockMainView, stubEntryService, stubDialogService);

        var entryToAdd = new PhonebookEntry("John Doe", "123456789");

        // Act
        stubEntryService.EntryAdded += Raise.Event<EventHandler<PhonebookEntry>>(this, entryToAdd);

        // Assert
        Assert.IsTrue(mockMainView.Entries.Contains(entryToAdd));
    }
}
