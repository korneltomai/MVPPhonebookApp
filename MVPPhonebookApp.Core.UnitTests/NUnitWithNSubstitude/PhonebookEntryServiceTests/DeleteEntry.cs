using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.PhonebookEntryServiceTests;

[TestFixture]
public class DeleteEntry
{
    [Test]
    public void WhenCalledWithValidEntries_CallsAddEntryFromRepository()
    {
        // Arrange
        var mockRepository = Substitute.For<IPhonebookRepository>();
        mockRepository.EntryExists(Arg.Any<PhonebookEntry>()).Returns(true);
        var service = new PhonebookEntryService(mockRepository);

        var entryToDelete = new PhonebookEntry("John Doe", "123456789");

        // Act
        service.DeleteEntry(entryToDelete);

        // Assert
        mockRepository.Received().DeleteEntry(entryToDelete);
    }

    [Test]
    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        stubRepository.EntryExists(Arg.Any<PhonebookEntry>()).Returns(true);
        stubRepository.When(r => r.DeleteEntry(Arg.Any<PhonebookEntry>()))
            .Do(info => { throw new Exception("Fake exception"); });
        var service = new PhonebookEntryService(stubRepository);

        var entryToDelete = new PhonebookEntry("John Doe", "123456789");

        // Act + Assert
        Assert.Throws<Exception>(() => service.DeleteEntry(entryToDelete), 
            "Fake exception");
    }

    [Test]
    public void WhenRepositoryDoesNotContainEntry_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var service = new PhonebookEntryService(stubRepository);

        var entryToDelete = new PhonebookEntry("John Doe", "123456789");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.DeleteEntry(entryToDelete),
            "The entry to delete does not exist.");
    }
}
