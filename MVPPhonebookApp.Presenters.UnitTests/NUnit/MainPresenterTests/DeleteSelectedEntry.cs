using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.UnitTests.Fakes;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnit.MainPresenterTests;

[TestFixture]
public class DeleteSelectedEntry
{
    [Test]
    public void WhenSelectedEntryIsNotNull_CallsDeleteEntryFromService()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var mockPhonebookEntryService = new FakePhonebookEntryService();
        var mainPresenter = new MainPresenter(stubMainView, mockPhonebookEntryService);

        stubMainView.SelectedEntry = new PhonebookEntry("Fake Entry", "123456789");

        // Act  
        stubMainView.TriggerDeleteEntryClicked();

        // Assert
        Assert.That(mockPhonebookEntryService.DeleteEntryCalled, Is.True);
    }

    [Test]
    public void WhenServiceThrowsException_SendsErrorToView()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var stubPhonebookEntryService = new FakePhonebookEntryService();
        stubPhonebookEntryService.DeleteEntryWillThrow = new Exception("Fake exception");
        var mainPresenter = new MainPresenter(stubMainView, stubPhonebookEntryService);

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
        var stubPhonebookEntryService = new FakePhonebookEntryService();
        var mainPresenter = new MainPresenter(stubMainView, stubPhonebookEntryService);

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
        var stubPhonebookEntryService = new FakePhonebookEntryService();
        var mainPresenter = new MainPresenter(mockMainView, stubPhonebookEntryService);

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
