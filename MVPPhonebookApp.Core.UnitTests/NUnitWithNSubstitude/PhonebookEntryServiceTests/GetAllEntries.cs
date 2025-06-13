using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.PhonebookEntryServiceTests;

[TestFixture]
public class GetAllEntries
{
    [Test]
    public void WhenCalled_CallsGetAllEntriesFromRepository()
    {
        // Arrange
        var mockRepository = Substitute.For<IPhonebookRepository>();
        var service = new PhonebookEntryService(mockRepository);

        // Act
        service.GetAllEntries();

        // Assert
        mockRepository.Received().GetAllEntries();
    }

    [Test]
    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        stubRepository.When(r => r.GetAllEntries())
            .Do(info => { throw new Exception("Fake exception"); });
        var service = new PhonebookEntryService(stubRepository);

        // Act + Assert
        Assert.Throws<Exception>(() => service.GetAllEntries(), 
            "Fake exception");
    }
}
