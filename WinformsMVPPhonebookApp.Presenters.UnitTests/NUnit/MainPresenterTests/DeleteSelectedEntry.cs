using WinformsMVPPhonebookApp.Core.Models;
using WinformsMVPPhonebookApp.Presenters.Presenters;
using WinformsMVPPhonebookApp.Presenters.UnitTests.Fakes;

namespace WinformsMVPPhonebookApp.Presenters.UnitTests.NUnit.MainPresenterTests;

[TestFixture]
public class DeleteSelectedEntry
{
    [Test]
    public void WhenSelectedEntryIsNotNull_CallsDeleteEntryFromRepository()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var mockPhonebookRepository = new FakePhonebookRepository();
        var mainPresenter = new MainPresenter(stubMainView, mockPhonebookRepository);

        stubMainView.SelectedEntry = new PhonebookEntry("Fake Entry", "123456789");

        // Act  
        stubMainView.TriggerDeleteEntryClicked();

        // Assert
        Assert.That(mockPhonebookRepository.DeleteEntryCalled, Is.True);
    }

    [Test]
    public void WhenRepositoryThrowsException_SendsErrorToView()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var mockPhonebookRepository = new FakePhonebookRepository();
        mockPhonebookRepository.DeleteEntryWillThrow = new Exception("Fake exception");
        var mainPresenter = new MainPresenter(stubMainView, mockPhonebookRepository);

        stubMainView.SelectedEntry = new PhonebookEntry("Fake Entry", "123456789");

        // Act  
        stubMainView.TriggerDeleteEntryClicked();

        // Assert
        Assert.That(stubMainView.ErrorMessage, Contains.Substring("Fake exception"));
    }

    [Test]
    public void WhenSelectedEntryIsNull_SendsErrorToView()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var mockPhonebookRepository = new FakePhonebookRepository();
        var mainPresenter = new MainPresenter(stubMainView, mockPhonebookRepository);

        stubMainView.SelectedEntry = null;

        // Act  
        stubMainView.TriggerDeleteEntryClicked();

        // Assert
        Assert.That(stubMainView.ErrorMessage, Contains.Substring("No entry selected for deletion"));
    }

    [Test]
    public void WhenSelectedEntryIsNotNull_RemovesEntryFromView()
    {
        // Arrange
        var mockMainView = new FakeMainView
        {
            Entries = [
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry ("Jane Smith", "987654321")
            ]
        };
        mockMainView.SelectedEntry = mockMainView.Entries.First();
        var stubPhonebookRepository = new FakePhonebookRepository();
        var mainPresenter = new MainPresenter(mockMainView, stubPhonebookRepository);

        var expectedEntries = new List<PhonebookEntry>
        {
            new PhonebookEntry("Jane Smith", "987654321")
        };

        // Act
        mockMainView.TriggerDeleteEntryClicked();

        // Assert
        Assert.That(mockMainView.Entries.Count, Is.EqualTo(expectedEntries.Count));
        Assert.That(mockMainView.Entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }
}
