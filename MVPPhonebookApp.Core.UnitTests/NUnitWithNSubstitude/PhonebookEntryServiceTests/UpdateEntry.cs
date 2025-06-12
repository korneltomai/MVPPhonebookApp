using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.Services;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.PhonebookEntryServiceTests;

[TestFixture]
public class UpdateEntry
{
    public void WhenCalled_CallsUpdateEntryFromRepository()
    {
        // Arrange
        var mockRepository = Substitute.For<IPhonebookRepository>();
        var service = Substitute.For<PhonebookEntryService>(mockRepository);

        var oldEntry = new PhonebookEntry("Fake Entry", "123456789");
        var newEntry = new PhonebookEntry("Another Fake Entry", "987654321");

        // Act
        service.UpdateEntry(oldEntry, newEntry);

        // Assert
        mockRepository.Received().UpdateEntry(oldEntry, newEntry);
    }

    public void WhenRepositoryThrows_ThrowsExceptionFurther()
    {
        // Arrange
        var stubRepository = Substitute.For<IPhonebookRepository>();
        stubRepository.When(r => r.UpdateEntry(Arg.Any<PhonebookEntry>(), Arg.Any<PhonebookEntry>()))
            .Do(info => { throw new Exception("Fake exception"); });
        var service = Substitute.For<PhonebookEntryService>(stubRepository);

        var oldEntry = new PhonebookEntry("Fake Entry", "123456789");
        var newEntry = new PhonebookEntry("Another Fake Entry", "987654321");

        // Act + Assert
        Assert.Throws<Exception>(() => service.UpdateEntry(oldEntry, newEntry), "Fake exception");
    }
}
