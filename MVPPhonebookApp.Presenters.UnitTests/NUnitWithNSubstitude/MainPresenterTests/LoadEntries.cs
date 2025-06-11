using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Presenters.Presenters;
using MVPPhonebookApp.Presenters.Views;

namespace MVPPhonebookApp.Presenters.UnitTests.NUnitWithNSubstitude.MainPresenterTests;

[TestFixture]
public class LoadEntries
{
    [Test]
    public void WhenCalled_CallsGetAllEntriesFromRepository()
    {
        // Arrange
        var stubMainView = Substitute.For<IMainView>();
        var mockPhonebookRepository = Substitute.For<IPhonebookRepository>();
        var mainPresenter = new MainPresenter(stubMainView, mockPhonebookRepository);

        // Act  
        mainPresenter.LoadEntries();

        // Assert
        mockPhonebookRepository.Received().GetAllEntries();
    }

    [Test]
    public void WhenGotValidEntriesFromRepository_AddsEntriesToView()
    {
        // Arrange
        var mockMainView = Substitute.For<IMainView>();
        var stubPhonebookRepository = Substitute.For<IPhonebookRepository>();
        stubPhonebookRepository.GetAllEntries().Returns(_ =>
            [
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry("Jane Smith", "987654321")
            ]
        );
        var mainPresenter = new MainPresenter(mockMainView, stubPhonebookRepository);

        var expectedEntries = new List<PhonebookEntry>
            {
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry("Jane Smith", "987654321")
            };

        // Act
        mainPresenter.LoadEntries();

        // Assert
        Assert.That(expectedEntries.Count, Is.EqualTo(mockMainView.Entries.Count));
        Assert.That(mockMainView.Entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }
}