using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.UnitTests.Fakes;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnit.MainPresenterTests;

[TestFixture]
public class LoadEntries
{
    [Test]
    public void WhenCalled_CallsGetAllEntriesFromService()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var stubPhonebookEntryService = new FakePhonebookEntryService();
        var mainPresenter = new MainPresenter(stubMainView, stubPhonebookEntryService);

        // Act  
        mainPresenter.LoadEntries();

        // Assert
        Assert.That(stubPhonebookEntryService.GetAllEntriesCalled, Is.True);
    }

    [Test]
    public void WhenGotValidEntriesFromService_AddsEntriesToView()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var stubPhonebookEntryService = new FakePhonebookEntryService
        {
            Entries =
            [
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry ("Jane Smith", "987654321")
            ]
        };
        var mainPresenter = new MainPresenter(stubMainView, stubPhonebookEntryService);

        var expectedEntries = new List<PhonebookEntry>
            {
                new("John Doe", "123456789"),
                new("Jane Smith", "987654321")
            };


        // Act
        mainPresenter.LoadEntries();

        // Assert
        Assert.That(stubMainView.Entries.Count, Is.EqualTo(expectedEntries.Count));
        Assert.That(stubMainView.Entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }
}