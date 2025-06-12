using NSubstitute;
using MVPPhonebookApp.Core.FileSystem;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.UnitTests.Helpers;

namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.CsvPhonebookRepositoryTests;

public class DeleteEntry
{
    [Test]
    public void WhenEntryExists_DeletesEntry()
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
        string fileContent = "John Doe,123456789\r\nJane Smith,987654321";
        stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));
        stubFileSystem.CreateFile(Arg.Any<string>()).Returns(new WriteBackStream(content => fileContent = content));

        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var entryToDelete = new PhonebookEntry("John Doe", "123456789");
        var expectedEntries = new List<PhonebookEntry>
        {
            new PhonebookEntry("Jane Smith", "987654321")
        };

        // Act
        repository.DeleteEntry(entryToDelete);

        // Assert
        var entries = repository.GetAllEntries().ToList();

        Assert.That(expectedEntries.Count, Is.EqualTo(entries.Count));
        Assert.That(entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }

    [Test]
    public void WhenEntryDoesNotExist_ThrowsInvalidOperationException()
    {
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
        string fileContent = "John Doe,123456789\r\nJane Smith,987654321";
        stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));

        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var entryToDelete = new PhonebookEntry("Does not exist", "999999999");

        // Assert + Act
        Assert.Throws<InvalidOperationException>(() => repository.DeleteEntry(entryToDelete), 
            "The entry to delete does not exist.");
    }

    [Test]
    public void WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(false);

        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var entryToDelete = new PhonebookEntry("Does not exist", "999999999");

        // Assert + Act
        Assert.Throws<FileNotFoundException>(() => repository.DeleteEntry(entryToDelete), 
            "The entries file does not exist.");
    }
}
