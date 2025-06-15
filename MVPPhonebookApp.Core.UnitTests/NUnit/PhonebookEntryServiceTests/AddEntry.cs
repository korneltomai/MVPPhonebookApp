using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;
using System.Reflection.Metadata;

namespace MVPPhonebookApp.Core.UnitTests.NUnit.PhonebookEntryServiceTests;

[TestFixture]
public class AddEntry
{
    [Test]
    public void WhenCalledWithValidEntries_CallsAddEntryFromRepository()
    {
        // Arrange
        var mockRepository = new FakePhonebookRepository();
        var service = new PhonebookEntryService(mockRepository);

        var entryToAdd = new PhonebookEntry("Fake Entry", "333333333");

        // Act
        service.AddEntry(entryToAdd);

        // Assert
        Assert.That(mockRepository.AddEntryCalled, Is.True);
        Assert.That(mockRepository.AddEntryParameter, Is.EqualTo(entryToAdd));
    }

    [Test]
    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.AddEntryWillThrow = new Exception("Fake exception");
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("Fake Entry", "123456789");

        // Act + Assert
        Assert.Throws<Exception>(() => service.AddEntry(entryToAdd), 
            "Fake exception");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntry_ThrowsValidationException()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.Entries =
            [
                new PhonebookEntry("John Doe", "123456789")
            ];
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("John Doe", "123456789");

        // Assert + Act
        Assert.Throws<ValidationException>(() => service.AddEntry(entryToAdd), 
            "An entry with the same values already exists.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntryWithSameName_ThrowsValidationException()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.Entries =
            [
                new PhonebookEntry("John Doe", "123456789")
            ];
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("John Doe", "987654321");

        // Assert + Act
        Assert.Throws<ValidationException>(() => service.AddEntry(entryToAdd), 
            "An entry with the same name already exists.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntryWithSamePhoneNumber_ThrowsValidationException()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.Entries =
            [
                new PhonebookEntry("John Doe", "123456789")
            ];
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("Jane Smith", "123456789");

        // Assert + Act
        Assert.Throws<ValidationException>(() => service.AddEntry(entryToAdd), 
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
