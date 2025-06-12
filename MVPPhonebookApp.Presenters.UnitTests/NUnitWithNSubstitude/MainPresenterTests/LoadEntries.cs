using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.Views;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Presenters.UnitTests.Fakes;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnitWithNSubstitude.MainPresenterTests;

[TestFixture]
public class LoadEntries
{
    [Test]
    public void WhenCalled_CallsGetAllEntriesFromService()
    {
        // Arrange
        var stubMainView = Substitute.For<IMainView>();
        var mockPhonebookEntryService = Substitute.For<PhonebookEntryService>(new EmptyPhonebookRepository());
        var mainPresenter = new MainPresenter(stubMainView, mockPhonebookEntryService);

        // Act  
        mainPresenter.LoadEntries();

        // Assert
        mockPhonebookEntryService.Received().GetAllEntries();
    }

    [Test]
    public void WhenGotValidEntriesFromService_AddsEntriesToView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        var stubPhonebookEntryService = Substitute.For<PhonebookEntryService>(new EmptyPhonebookRepository());
        stubPhonebookEntryService.GetAllEntries().Returns(_ =>
            [
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry("Jane Smith", "987654321")
            ]
        );
        var mainPresenter = new MainPresenter(mockMainView, stubPhonebookEntryService);

        var expectedEntries = new List<PhonebookEntry>
            {
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry("Jane Smith", "987654321")
            };

        // Act
        mainPresenter.LoadEntries();

        // Assert
        Assert.That(mockMainView.Entries.Count, Is.EqualTo(expectedEntries.Count));
        Assert.That(mockMainView.Entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }
}