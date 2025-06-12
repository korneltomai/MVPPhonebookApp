using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnit.CsvPhonebookRepositoryTests;

[TestFixture]
public class UpdateEntry
{
    [Test]
    public void WhenFileExistsAndContainsEntry_UpdatesEntry()
    {
        // Arrange
        var stubFileSystem = new FakeFileSystem
        {
            DoesFileExists = true,
            FileContent = "John Doe,123456789\r\nJane Smith,987654321"
        };
        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Alice Johnson", "5551234567");
        var expectedEntries = new List<PhonebookEntry>
        {
            new PhonebookEntry("Alice Johnson", "5551234567"),
            new PhonebookEntry("Jane Smith", "987654321"),
        };

        // Act
        repository.UpdateEntry(oldEntry, newEntry);

        // Assert
        var entries = repository.GetAllEntries().ToList();

        Assert.That(expectedEntries.Count, Is.EqualTo(entries.Count));
        Assert.That(entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }

    [Test]
    public void WhenFileExistsAndDoesNotContainEntry_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubFileSystem = new FakeFileSystem
        {
            DoesFileExists = true,
            FileContent = "John Doe,123456789\r\nJane Smith,987654321"
        };
        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var oldEntry = new PhonebookEntry("Alice Johnson", "5551234567");
        var newEntry = new PhonebookEntry("Bob Big", "1234567555");

        // Assert + Act
        Exception exception = Assert.Throws<InvalidOperationException>(() => repository.UpdateEntry(oldEntry, newEntry));
        Assert.That(exception.Message, Contains.Substring("The entry to update does not exist"));
    }

    [TestCase("Alice Johnson", "333333333")]
    [TestCase("Does not exist", "5551234567")]
    [TestCase("Alice Johnson", "5551234567")]
    public void WhenFileExistsAndAlreadyContainsSimilarEntry_ThrowsInvalidOperationException(string name, string phoneNumber)
    {
        // Arrange
        var stubFileSystem = new FakeFileSystem
        {
            DoesFileExists = true,
            FileContent = $"John Doe,123456789\r\n{name},{phoneNumber}"
        };
        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Alice Johnson", "5551234567");

        // Assert + Act
        Exception exception = Assert.Throws<InvalidOperationException>(() => repository.UpdateEntry(oldEntry, newEntry));
        Assert.That(exception.Message, Does.Contain("An entry with the same").And.Contain("already exists"));
    }

    [Test]
    public void WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var stubFileSystem = new FakeFileSystem
        {
            DoesFileExists = false,
        };
        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Alice Johnson", "5551234567");

        // Assert + Act
        Exception exception = Assert.Throws<FileNotFoundException>(() => repository.UpdateEntry(oldEntry, newEntry));
        Assert.That(exception.Message, Contains.Substring("The entries file does not exist"));
    }
}
