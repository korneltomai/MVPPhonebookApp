using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.PhonebookEntryServiceTests;

[TestFixture]
public class GetAllEntries
{
    public void WhenCalled_CallsGetAllEntriesFromRepository()
    {
        // Arrange
        var mockRepository = Substitute.For<IPhonebookRepository>();
        var service = Substitute.For<PhonebookEntryService>(mockRepository);

        // Act
        service.GetAllEntries();

        // Assert
        mockRepository.Received().GetAllEntries();
    }

    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        stubRepository.When(r => r.AddEntry(Arg.Any<PhonebookEntry>()))
            .Do(info => { throw new Exception("Fake exception"); });
        var service = Substitute.For<PhonebookEntryService>(stubRepository);

        // Act + Assert
        Assert.Throws<Exception>(() => service.GetAllEntries(), "Fake exception");
    }
}
