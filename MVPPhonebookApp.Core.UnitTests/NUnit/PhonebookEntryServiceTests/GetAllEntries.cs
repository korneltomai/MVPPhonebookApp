using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnit.PhonebookEntryServiceTests;

[TestFixture]
public class GetAllEntries
{
    public void WhenCalled_CallsGetAllEntriesFromRepository()
    {
        // Arrange
        var mockRepository = new FakePhonebookRepository();
        var service = new PhonebookEntryService(mockRepository);

        // Act
        service.GetAllEntries();

        // Assert
        Assert.That(mockRepository.GetAllEntriesCalled, Is.True);
    }

    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = new FakePhonebookRepository();
        stubRepository.GetAllEntriesWillThrow = new Exception("Fake exception");
        var service = new PhonebookEntryService(stubRepository);

        // Act + Assert
        Assert.Throws<Exception>(() => service.GetAllEntries(), "Fake exception");
    }
}
