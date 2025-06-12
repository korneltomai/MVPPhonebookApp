using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnit.PhonebookEntryServiceTests;

[TestFixture]
public class DeleteEntry
{
    public void WhenCalled_CallsDeleteEntryFromRepository()
    {
        // Arrange
        var mockRepository = new FakePhonebookRepository();
        var service = new PhonebookEntryService(mockRepository);

        var entry = new PhonebookEntry("Fake Entry", "123456789");

        // Act
        service.DeleteEntry(entry);

        // Assert
        Assert.That(mockRepository.DeleteEntryCalled, Is.True);
        Assert.That(mockRepository.DeleteEntryParameter, Is.EqualTo(entry));
    }

    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.DeleteEntryWillThrow = new Exception("Fake exception");
        var service = new PhonebookEntryService(stubRepository);

        var entry = new PhonebookEntry("Fake Entry", "123456789");

        // Act + Assert
        Assert.Throws<Exception>(() => service.DeleteEntry(entry), "Fake exception");
    }
}
