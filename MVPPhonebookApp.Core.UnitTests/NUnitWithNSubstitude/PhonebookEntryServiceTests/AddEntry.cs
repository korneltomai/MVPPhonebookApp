using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.Repository;

namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.PhonebookEntryServiceTests;

[TestFixture]
public class AddEntry
{
    [Test]
    public void WhenCalledWithValidEntries_CallsAddEntryFromRepository()
    {
        // Arrange
        var mockRepository = Substitute.For<IPhonebookRepository>();
        var service = new PhonebookEntryService(mockRepository);

        var entryToAdd = new PhonebookEntry("John Doe", "123456789");

        // Act
        service.AddEntry(entryToAdd);

        // Assert
        mockRepository.Received().AddEntry(entryToAdd);
    }

    [Test]
    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        stubRepository.When(r => r.AddEntry(Arg.Any<PhonebookEntry>()))
            .Do(info => { throw new Exception("Fake exception"); });
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("John Doe", "123456789");

        // Act + Assert
        Assert.Throws<Exception>(() => service.AddEntry(entryToAdd), 
            "Fake exception");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntry_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        stubRepository.EntryExists(Arg.Any<PhonebookEntry>()).Returns(true);

        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("John Doe", "123456789");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.AddEntry(entryToAdd), "An entry with the same values already exists.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntryWithSameName_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        stubRepository.EntryExistsByName(Arg.Any<string>()).Returns(true);
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("John Doe", "333333333");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.AddEntry(entryToAdd), "An entry with the same name already exists.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntryWithSamePhoneNumber_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        stubRepository.EntryExistsByPhoneNumber(Arg.Any<string>()).Returns(true);
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("John Doe", "123456789");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.AddEntry(entryToAdd),
            "An entry with the same phone number already exists.");
    }

    [TestCase("", "123456789")]
    [TestCase("John Doe", "")]
    public void WhenGetsEntryWithEmptyNameOrPhoneNumber_ThrowsInvalidOperationException(string name, string phoneNumber)
    {
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry(name, phoneNumber);

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.AddEntry(entryToAdd),
            "Name and phone number cannot be empty.");
    }

    [Test]
    public void WhenGetsEntryWithLongName_ThrowsInvalidOperationException()
    {
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("This is a name that is over 32 characters long", "123456789");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.AddEntry(entryToAdd),
            "Name cannot exceed 32 characters.");
    }

    [Test]
    public void WhenGetsEntryWithLongPhoneNumber_ThrowsInvalidOperationException()
    {
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var service = new PhonebookEntryService(stubRepository);

        var entryToAdd = new PhonebookEntry("John Doe", "This is a phone number that is over 32 characters long");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.AddEntry(entryToAdd),
            "Phone number cannot exceed 32 characters.");
    }
}
