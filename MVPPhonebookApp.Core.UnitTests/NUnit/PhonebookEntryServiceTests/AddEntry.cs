using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnit.PhonebookEntryServiceTests;

[TestFixture]
public class AddEntry
{
    public void WhenCalled_CallsAddEntryFromRepository()
    {
        // Arrange
        var mockRepository = new FakePhonebookRepository();
        var service = new PhonebookEntryService(mockRepository);

        var entry = new PhonebookEntry("Fake Entry", "123456789");

        // Act
        service.AddEntry(entry);

        // Assert
        Assert.That(mockRepository.AddEntryCalled, Is.True);
        Assert.That(mockRepository.AddEntryParameter, Is.EqualTo(entry));
    }

    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var mockRepository = new FakePhonebookRepository();
        mockRepository.AddEntryWillThrow = new Exception("Fake exception");
        var service = new PhonebookEntryService(mockRepository);

        var entry = new PhonebookEntry("Fake Entry", "123456789");

        // Act + Assert
        Assert.Throws<Exception>(() => service.AddEntry(entry), "Fake exception");
    }
}
