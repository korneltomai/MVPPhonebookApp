using NSubstitute;
using WinformsMVPPhonebookApp.Core.Models;
using WinformsMVPPhonebookApp.Core.Repository;
using WinformsMVPPhonebookApp.Presenters.Presenters;
using WinformsMVPPhonebookApp.Presenters.Views;

namespace WinformsMVPPhonebookApp.Presenters.UnitTests.NUnitWithNSubstitude.MainPresenterTests;

[TestFixture]
public class DeleteSelectedEntry
{
    [Test]
    public void WhenSelectedEntryIsNotNull_CallsDeleteEntryFromRepository()
    {
        // Arrange
        var stubMainView = Substitute.For<IMainView>();
        stubMainView.SelectedEntry.Returns(_ => new PhonebookEntry("Fake Entry", "123456789"));
        var mockPhonebookRepository = Substitute.For<IPhonebookRepository>();
        var mainPresenter = new MainPresenter(stubMainView, mockPhonebookRepository);

        // Act  
        stubMainView.DeleteEntryClicked += Raise.Event();

        // Assert
        mockPhonebookRepository.Received().DeleteEntry(Arg.Is<PhonebookEntry>(e => 
            e.Name == stubMainView.SelectedEntry!.Name && 
            e.PhoneNumber == stubMainView.SelectedEntry!.PhoneNumber));
    }

    [Test]
    public void WhenRepositoryThrowsException_SendsErrorToView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        mockMainView.SelectedEntry.Returns(_ => new PhonebookEntry("Fake Entry", "123456789"));
        var stubPhonebookRepository = Substitute.For<IPhonebookRepository>();
        stubPhonebookRepository.When(r => r.DeleteEntry(Arg.Any<PhonebookEntry>()))
            .Do(info => { throw new Exception("Fake exception"); });
        var mainPresenter = new MainPresenter(mockMainView, stubPhonebookRepository);

        // Act  
        mockMainView.DeleteEntryClicked += Raise.Event();

        // Assert
        mockMainView.Received().ShowError(Arg.Is<string>(s => s.Contains("Fake exception")));
    }

    [Test]
    public void WhenSelectedEntryIsNull_SendsErrorToView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        mockMainView.SelectedEntry.Returns(_ => null);
        var stubPhonebookRepository = Substitute.For<IPhonebookRepository>();
        var mainPresenter = new MainPresenter(mockMainView, stubPhonebookRepository);

        // Act  
        mockMainView.DeleteEntryClicked += Raise.Event();

        // Assert
        mockMainView.Received().ShowError(Arg.Is<string>(s => s.Contains("No entry selected for deletion")));
    }

    [Test]
    public void WhenSelectedEntryIsNotNull_RemovesEntryView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        mockMainView.Entries =
            [
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry ("Jane Smith", "987654321")
            ];
        mockMainView.SelectedEntry.Returns(_ => mockMainView.Entries.First());
        var stubPhonebookRepository = Substitute.For<IPhonebookRepository>();
        var mainPresenter = new MainPresenter(mockMainView, stubPhonebookRepository);

        var expectedEntries = new List<PhonebookEntry>
        {
            new PhonebookEntry("Jane Smith", "987654321")
        };

        // Act
        mockMainView.DeleteEntryClicked += Raise.Event();

        // Assert
        Assert.That(expectedEntries.Count, Is.EqualTo(mockMainView.Entries.Count));
        Assert.That(mockMainView.Entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }
}
