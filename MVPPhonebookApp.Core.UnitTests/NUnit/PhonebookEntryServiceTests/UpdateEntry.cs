using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnit.PhonebookEntryServiceTests;

[TestFixture]
public class UpdateEntry
{
    [Test]
    public void WhenCalledWithValidEntries_CallsUpdateEntryFromRepository()
    {
        // Arrange
        var mockRepository = new FakePhonebookRepository();
        mockRepository.Entries =
            [
                new PhonebookEntry("John Doe", "123456789")
            ];
        var service = new PhonebookEntryService(mockRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        // Act
        service.UpdateEntry(oldEntry, newEntry);

        // Assert
        Assert.That(mockRepository.UpdateEntryCalled, Is.True);
        Assert.That(mockRepository.UpdateEntryParameters.oldEntry, Is.EqualTo(oldEntry));
        Assert.That(mockRepository.UpdateEntryParameters.newEntry, Is.EqualTo(newEntry));
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
        stubRepository.UpdateEntryWillThrow = new Exception("Fake exception");
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        // Act + Assert
        Assert.Throws<Exception>(() => service.UpdateEntry(oldEntry, newEntry), 
            "Fake exception");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntry_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.Entries =
            [
                new PhonebookEntry("Jane Smith", "987654321")
            ];
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.UpdateEntry(oldEntry, newEntry), 
            "An entry with the same values already exists.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntryWithSameName_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.Entries =
            [
                new PhonebookEntry("Jane Smith", "333333333")
            ];
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.UpdateEntry(oldEntry, newEntry),
           "An entry with the same name already exists.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntryWithSamePhoneNumber_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.Entries =
            [
                new PhonebookEntry("Alice Johnson", "987654321")
            ];
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.UpdateEntry(oldEntry, newEntry),
           "An entry with the same phone number already exists.");
    }
}
