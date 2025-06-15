using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.UnitTests.Fakes;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnit.MainPresenterTests;

public class OnEntryUpdated
{
    [Test]
    public void WhenCalled_UpdatesEntryInView()
    {
        // Arrange
        var mockMainView = new FakeMainView();
        var stubEntryService = new FakePhonebookEntryService();
        var stubDialogService = new FakeAddOrEditDialogService();
        var presenter = new MainPresenter(mockMainView, stubEntryService, stubDialogService);

        mockMainView.Entries =
            [
                new PhonebookEntry("John Doe", "123456789")
            ];

        var oldEntry = mockMainView.Entries[0];
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        var expectedEntries = new List<PhonebookEntry>
            {
                new("Jane Smith", "987654321")
            };

        // Act
        stubEntryService.OnEntryUpdated(oldEntry, newEntry);

        // Assert
        Assert.That(expectedEntries.Count, Is.EqualTo(mockMainView.Entries.Count));
        Assert.That(mockMainView.Entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }
}
