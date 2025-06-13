using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.PhonebookEntryServiceTests;

[TestFixture]
public class UpdateEntry
{
    [Test]
    public void WhenCalledWithValidEntries_CallsUpdateEntryFromRepository()
    {
        // Arrange
        var mockRepository = Substitute.For<IPhonebookRepository>();
        var service = new PhonebookEntryService(mockRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        mockRepository.EntryExists(oldEntry).Returns(true);

        // Act
        service.UpdateEntry(oldEntry, newEntry);

        // Assert
        mockRepository.Received().UpdateEntry(oldEntry, newEntry);
    }

    [Test]
    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        stubRepository.When(r => r.UpdateEntry(Arg.Any<PhonebookEntry>(), Arg.Any<PhonebookEntry>()))
            .Do(info => { throw new Exception("Fake exception"); });
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        stubRepository.EntryExists(oldEntry).Returns(true);

        // Act + Assert
        Assert.Throws<Exception>(() => service.UpdateEntry(oldEntry, newEntry), "Fake exception");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntry_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        stubRepository.EntryExists(oldEntry).Returns(true);
        stubRepository.EntryExists(newEntry).Returns(true);

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.UpdateEntry(oldEntry, newEntry),
            "An entry with the same values already exists.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntryWithSameName_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        stubRepository.EntryExists(oldEntry).Returns(true);
        stubRepository.EntryExistsByName(newEntry.Name).Returns(true);

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.UpdateEntry(oldEntry, newEntry),
           "An entry with the same name already exists.");
    }

    [Test]
    public void WhenRepositoryAlreadyContainsEntryWithSamePhoneNumber_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        var service = new PhonebookEntryService(stubRepository);

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Jane Smith", "987654321");

        stubRepository.EntryExists(oldEntry).Returns(true);
        stubRepository.EntryExistsByPhoneNumber(newEntry.PhoneNumber).Returns(true);

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => service.UpdateEntry(oldEntry, newEntry),
           "An entry with the same phone number already exists.");
    }
}
