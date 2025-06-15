using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.Services;
using MVPPhonebookApp.Presenters.Views;
using NSubstitute;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnitWithNSubstitude.MainPresenterTests;

public class OnEntryUpdated
{
    [Test]
    public void WhenCalled_UpdatesEntryInView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        mockMainView.Entries.Returns(new List<PhonebookEntry> { new PhonebookEntry("John Doe", "123456789") });
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var stubEntryService = Substitute.For<PhonebookEntryService>(stubRepository);
        var stubDialogService = Substitute.For<IAddOrEditDialogService>();
        var presenter = new MainPresenter(mockMainView, stubEntryService, stubDialogService);

        var oldEntry = mockMainView.Entries[0];
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        var expectedEntries = new List<PhonebookEntry>
            {
                new("Jane Smith", "987654321")
            };

        // Act
        stubEntryService.EntryUpdated += Raise.Event<EventHandler<(PhonebookEntry, PhonebookEntry)>>(this, (oldEntry, newEntry));

        // Assert
        Assert.That(expectedEntries.Count, Is.EqualTo(mockMainView.Entries.Count));
        Assert.That(mockMainView.Entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }
}
