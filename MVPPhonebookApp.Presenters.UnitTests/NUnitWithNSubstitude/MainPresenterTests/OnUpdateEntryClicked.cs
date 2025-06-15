using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.Services;
using MVPPhonebookApp.Presenters.Views;
using NSubstitute;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnitWithNSubstitude.MainPresenterTests;

public class OnUpdateEntryClicked
{
    [Test]
    public void WhenSelectedEntryIsNotNull_ShowsAddOrEditDialog()
    {
        // Arrange
        var stubMainView = Substitute.For<IMainView>();
        stubMainView.SelectedEntry.Returns(new PhonebookEntry("John Doe", "123456789"));
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var stubEntryService = Substitute.For<PhonebookEntryService>(stubRepository);
        var mockDialogService = Substitute.For<IAddOrEditDialogService>();
        var presenter = new MainPresenter(stubMainView, stubEntryService, mockDialogService);

        // Act  
        stubMainView.UpdateEntryClicked += Raise.Event();

        // Assert
        mockDialogService.Received().ShowAddOrEditDialog(
            Arg.Is<PhonebookEntry>(e => e.Name == "John Doe" && e.PhoneNumber == "123456789"));
    }

    [Test]
    public void WhenSelectedEntryIsNull_SendsErrorToView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var stubEntryService = Substitute.For<PhonebookEntryService>(stubRepository);
        var mockDialogService = Substitute.For<IAddOrEditDialogService>();
        var presenter = new MainPresenter(mockMainView, stubEntryService, mockDialogService);

        // Act  
        mockMainView.UpdateEntryClicked += Raise.Event();

        // Assert
        mockMainView.Received().ShowError("No entry selected for editing.");
    }
}
