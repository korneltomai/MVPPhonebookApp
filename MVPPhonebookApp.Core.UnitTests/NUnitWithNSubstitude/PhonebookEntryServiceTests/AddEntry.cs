using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;
using MVPPhonebookApp.Core.Repository;

namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.PhonebookEntryServiceTests;

[TestFixture]
public class AddEntry
{
    public void WhenCalled_CallsAddEntryFromRepository()
    {
        // Arrange
        var mockRepository = Substitute.For<IPhonebookRepository>();
        var service = Substitute.For<PhonebookEntryService>(mockRepository);

        var entry = new PhonebookEntry("Fake Entry", "123456789");

        // Act
        service.AddEntry(entry);

        // Assert
        mockRepository.Received().AddEntry(entry);
    }

    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        stubRepository.When(r => r.AddEntry(Arg.Any<PhonebookEntry>()))
            .Do(info => { throw new Exception("Fake exception"); });
        var service = Substitute.For<PhonebookEntryService>(stubRepository);

        var entry = new PhonebookEntry("Fake Entry", "123456789");

        // Act + Assert
        Assert.Throws<Exception>(() => service.AddEntry(entry), "Fake exception");
    }
}
