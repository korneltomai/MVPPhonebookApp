using NSubstitute;
using MVPPhonebookApp.Core.FileSystem;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.UnitTests.Helpers;

namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.CsvPhonebookRepositoryTests;

[TestFixture]
public class AddEntry
{
    [Test]
    public void WhenFileExists_AddsEntry()
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
        string fileContent = "John Doe,123456789\r\nJane Smith,987654321";
        stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));
        stubFileSystem.CreateFile(Arg.Any<string>()).Returns(new WriteBackStream(content => fileContent = content));

        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var entryToAdd = new PhonebookEntry("Alice Johnson", "5551234567");
        var expectedEntries = new List<PhonebookEntry>
        {
            new PhonebookEntry("John Doe", "123456789"),
            new PhonebookEntry("Jane Smith", "987654321"),
            new PhonebookEntry("Alice Johnson", "5551234567")
        };

        // Act
        repository.AddEntry(entryToAdd);

        // Assert
        var entries = repository.GetAllEntries().ToList();

        Assert.That(expectedEntries.Count, Is.EqualTo(entries.Count));
        Assert.That(entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }

    [TestCase("Does not exist yet", "123456789")]
    [TestCase("John Doe", "333333333")]
    [TestCase("John Doe", "123456789")]
    public void WhenFileExistsAndAlreadyContainsSimilarEntry_ThrowsInvalidOperationException(string name, string phoneNumber)
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
        string fileContent = "John Doe,123456789\r\nJane Smith,987654321\r\nAlice Johnson,5551234567";
        stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));
        stubFileSystem.CreateFile(Arg.Any<string>()).Returns(new WriteBackStream(content => fileContent = content));

        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var entryToAdd = new PhonebookEntry(name, phoneNumber);

        // Assert + Act
        Exception exception = Assert.Throws<InvalidOperationException>(() => repository.AddEntry(entryToAdd));
        Assert.That(exception.Message, Does.Contain("An entry with the same").And.Contain("already exists"));
    }

    [Test]
    public void WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(false);

        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var entryToAdd = new PhonebookEntry("Alice Johnson", "5551234567");

        // Assert + Act
        Exception exception = Assert.Throws<FileNotFoundException>(() => repository.DeleteEntry(entryToAdd));
        Assert.That(exception.Message, Contains.Substring("The entries file does not exist"));
    }
}
