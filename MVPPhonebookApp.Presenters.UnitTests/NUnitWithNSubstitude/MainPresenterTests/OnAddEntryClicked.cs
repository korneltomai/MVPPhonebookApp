using NSubstitute;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.Services;
using MVPPhonebookApp.Presenters.Views;
using MVPPhonebookApp.Core.Repository;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnitWithNSubstitude.MainPresenterTests;

public class OnAddEntryClicked
{
    [Test]
    public void WhenCalled_ShowsAddOrEditDialog()
    {
        // Arrange
        var stubMainView = Substitute.For<IMainView>();
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var stubEntryService = Substitute.For<PhonebookEntryService>(stubRepository);
        var mockDialogService = Substitute.For<IAddOrEditDialogService>();
        var mainPresenter = new MainPresenter(stubMainView, stubEntryService, mockDialogService);

        // Act
        stubMainView.AddEntryClicked += Raise.Event();

        // Assert
        mockDialogService.Received().ShowAddOrEditDialog();
    }
}
