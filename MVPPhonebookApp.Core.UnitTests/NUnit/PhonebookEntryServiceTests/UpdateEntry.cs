using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnit.PhonebookEntryServiceTests;

[TestFixture]
public class UpdateEntry
{
    public void WhenCalled_CallsUpdateEntryFromRepository()
    {
        // Arrange
        var mockRepository = new FakePhonebookRepository();
        var service = new PhonebookEntryService(mockRepository);

        var oldEntry = new PhonebookEntry("Fake Entry", "123456789");
        var newEntry = new PhonebookEntry("Another Fake Entry", "987654321");

        // Act
        service.UpdateEntry(oldEntry, newEntry);

        // Assert
        Assert.That(mockRepository.UpdateEntryCalled, Is.True);
    }

    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var mockRepository = new FakePhonebookRepository();
        mockRepository.UpdateEntryWillThrow = new Exception("Fake exception");
        var service = new PhonebookEntryService(mockRepository);

        var oldEntry = new PhonebookEntry("Fake Entry", "123456789");
        var newEntry = new PhonebookEntry("Another Fake Entry", "987654321");

        // Act + Assert
        Assert.Throws<Exception>(() => service.UpdateEntry(oldEntry, newEntry), "Fake exception");
    }
}
