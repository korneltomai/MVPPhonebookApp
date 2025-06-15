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
    public void WhenRepositoryDoesNotContainOldEntry_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.UpdateEntry(oldEntry, newEntry),
            "The entry to update does not exist.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntry_ThrowsValidationException()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.Entries =
            [
                new PhonebookEntry("John Doe", "123456789"), 
                new PhonebookEntry("Jane Smith", "987654321")
            ];
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        // Assert + Act
        Assert.Throws<ValidationException>(() => service.UpdateEntry(oldEntry, newEntry), 
            "An entry with the same values already exists.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntryWithSameName_ThrowsValidationException()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.Entries =
            [
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry("Jane Smith", "333333333")
            ];
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        // Assert + Act
        Assert.Throws<ValidationException>(() => service.UpdateEntry(oldEntry, newEntry),
           "An entry with the same name already exists.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntryWithSamePhoneNumber_ThrowsValidationException()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.Entries =
            [
                new PhonebookEntry("John Doe", "123456789"),
                new PhonebookEntry("Alice Johnson", "987654321")
            ];
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        // Assert + Act
        Assert.Throws<ValidationException>(() => service.UpdateEntry(oldEntry, newEntry),
           "An entry with the same phone number already exists.");
    }

    [TestCase("", "123456789")]
    [TestCase("John Doe", "")]
    public void WhenGetsEntryWithEmptyNameOrPhoneNumber_ThrowsValidationException(string name, string phoneNumber)
    {
        var stubRepository = new FakePhonebookRepository();
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry(name, phoneNumber);

        // Assert + Act
        Assert.Throws<ValidationException>(() => service.AddEntry(entryToAdd),
            "Name and phone number cannot be empty.");
    }

    [Test]
    public void WhenGetsEntryWithLongName_ThrowsValidationException()
    {
        var stubRepository = new FakePhonebookRepository();
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("This is a name that is over 32 characters long", "123456789");

        // Assert + Act
        Assert.Throws<ValidationException>(() => service.AddEntry(entryToAdd),
            "Name cannot exceed 32 characters.");
    }

    [Test]
    public void WhenGetsEntryWithLongPhoneNumber_ThrowsValidationException()
    {
        var stubRepository = new FakePhonebookRepository();
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("John Doe", "This is a phone number that is over 32 characters long");

        // Assert + Act
        Assert.Throws<ValidationException>(() => service.AddEntry(entryToAdd),
            "Phone number cannot exceed 32 characters.");
    }
}
