using NSubstitute;
using MVPPhonebookApp.Core.FileSystem;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using System;

namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.CsvPhonebookRepositoryTests;

[TestFixture]
public class GetAllEntries
{
    [Test]
    public void WhenFileExists_ReturnsEntries()
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
        string fileContent = "John Doe,123456789\r\nJane Smith,987654321";
        stubFileSystem.OpenRead(Arg.Any<string>()).Returns(
            new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));

        var repository = new CsvPhonebookRepository(stubFileSystem, "fake.csv");

        var expectedEntries = new List<PhonebookEntry>
        {
            new PhonebookEntry("John Doe", "123456789"),
            new PhonebookEntry("Jane Smith", "987654321")
        };

        // Act
        var entries = repository.GetAllEntries().ToList();

        // Assert
        Assert.That(expectedEntries.Count, Is.EqualTo(entries.Count));
        Assert.That(entries, Has.All.Matches<PhonebookEntry>(entry =>
            expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
        ));
    }

    [Test]
    public void WhenFileExistsAndIsEmpty_ReturnsEmptyList()
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
        stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream());

        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        // Act
        var entries = repository.GetAllEntries().ToList();

        // Assert
        Assert.That(entries, Is.Empty);
    }

    [TestCase("", "123456789")]
    [TestCase("John Doe", "")]
    [TestCase("", "")]
    public void WhenFileExistsWithMissingValues_ThrowsInvalidOperationException(string name, string phoneNumber)
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
        string fileContent = $"John Doe,123456789\r\n{name},{phoneNumber}\r\nJane Smith,987654321";
        stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));

        var repository = new CsvPhonebookRepository(stubFileSystem, "fake.csv");

        // Act + Assert
        Exception exception = Assert.Throws<InvalidOperationException>(() => repository.GetAllEntries());
        Assert.That(exception.Message, Contains.Substring("Each line in the entries file must contain exactly two values."));
    }

    [Test]
    public void WhenFileExistsWithEmptyLines_ThrowsInvalidOperationException()
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
        string fileContent = $"John Doe,123456789\r\n\r\nJane Smith,987654321";
        stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));

        var repository = new CsvPhonebookRepository(stubFileSystem, "fake.csv");

        // Assert
        Exception exception = Assert.Throws<InvalidOperationException>(() => repository.GetAllEntries());
        Assert.That(exception.Message, Contains.Substring("The entries file must not contain empty lines"));
    }

    [Test]
    public void WhenFileDoesNotExists_ReturnEmptyList()
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(false);
        stubFileSystem.CreateFile(Arg.Any<string>()).Returns(_ => new MemoryStream());

        var repository = new CsvPhonebookRepository(stubFileSystem, "fake.csv");

        // Act
        var entries = repository.GetAllEntries().ToList();

        // Assert
        Assert.That(entries, Is.Empty);
    }

    [Test]
    public void WhenFileDoesNotExists_CreatesFile()
    {
        // Arrange
        var mockFileSystem = Substitute.For<IFileSystem>();
        mockFileSystem.FileExists(Arg.Any<string>()).Returns(false);
        mockFileSystem.CreateFile(Arg.Any<string>()).Returns(_ => new MemoryStream());

        var repository = new CsvPhonebookRepository(mockFileSystem, "fake.csv");

        // Act
        var _ = repository.GetAllEntries();

        // Assert
        mockFileSystem.Received().CreateFile(Arg.Any<string>());
    }
}
