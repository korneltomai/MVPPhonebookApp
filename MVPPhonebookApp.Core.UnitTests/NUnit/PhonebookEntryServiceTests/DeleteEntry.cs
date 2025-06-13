using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnit.PhonebookEntryServiceTests;

[TestFixture]
public class DeleteEntry
{
    [Test]
    public void WhenCalledWithValidEntries_CallsDeleteEntryFromRepository()
    {
        // Arrange
        var mockRepository = new FakePhonebookRepository();
        mockRepository.Entries =
            [
                new PhonebookEntry("John Doe", "123456789")
            ];
        var service = new PhonebookEntryService(mockRepository);

        var entryToDelete = new PhonebookEntry("John Doe", "123456789");

        // Act
        service.DeleteEntry(entryToDelete);

        // Assert
        Assert.That(mockRepository.DeleteEntryCalled, Is.True);
        Assert.That(mockRepository.DeleteEntryParameter, Is.EqualTo(entryToDelete));
    }

    [Test]
    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.Entries =
            [
                new PhonebookEntry("John Doe", "123456789")
            ];
        stubRepository.DeleteEntryWillThrow = new Exception("Fake exception");
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
        var stubRepository = new FakePhonebookRepository();
        var service = new PhonebookEntryService(stubRepository);

        var entryToDelete = new PhonebookEntry("John Doe", "123456789");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.DeleteEntry(entryToDelete),
            "The entry to delete does not exist.");
    }
}
