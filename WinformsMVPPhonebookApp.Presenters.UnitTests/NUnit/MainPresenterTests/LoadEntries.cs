using WinformsMVPPhonebookApp.Core.Models;
using WinformsMVPPhonebookApp.Presenters.Presenters;
using WinformsMVPPhonebookApp.Presenters.UnitTests.Fakes;

namespace WinformsMVPPhonebookApp.Presenters.UnitTests.NUnit.MainPresenterTests;

[TestFixture]
public class LoadEntries
{
    [Test]
    public void WhenCalled_CallsGetAllEntriesFromRepository()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var mockPhonebookRepository = new FakePhonebookRepository();
        var mainPresenter = new MainPresenter(stubMainView, mockPhonebookRepository);

        // Act  
        mainPresenter.LoadEntries();

        // Assert
        Assert.That(mockPhonebookRepository.GetAllEntriesCalled, Is.True);
    }

    [Test]
    public void WhenGotValidEntriesFromRepository_AddsEntriesToView()
    {
        // Arrange
        var stubMainView = new FakeMainView();
        var stubPhonebookRepository = new FakePhonebookRepository
        {
            Entries = new List<PhonebookEntry>
            {
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry ("Jane Smith", "987654321")
            }
        };
        var mainPresenter = new MainPresenter(stubMainView, stubPhonebookRepository);

        var expectedEntries = new List<PhonebookEntry>
            {
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry("Jane Smith", "987654321")
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