﻿namespace MVPPhonebookApp.Core.UnitTests.NUnitWithNSubstitude.CsvPhonebookRepositoryTests;

using NSubstitute;
using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.UnitTests.Fakes;
using MVPPhonebookApp.Core.FileSystem;
using MVPPhonebookApp.Core.UnitTests.Helpers;
using System.Xml.Linq;

[TestFixture]
public class UpdateEntry
{
    [Test]
    public void WhenFileExists_UpdatesEntry()
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
        string fileContent = "John Doe,123456789\r\nJane Smith,987654321";
        stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));
        stubFileSystem.CreateFile(Arg.Any<string>()).Returns(new WriteBackStream(content => fileContent = content));

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
    public void WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var stubFileSystem = Substitute.For<IFileSystem>();
        stubFileSystem.FileExists(Arg.Any<string>()).Returns(false);

        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var oldEntry = new PhonebookEntry("John Doe", "123456789");
        var newEntry = new PhonebookEntry("Alice Johnson", "5551234567");

        // Assert + Act
        Assert.Throws<FileNotFoundException>(() => repository.UpdateEntry(oldEntry, newEntry),
            "The entries file does not exist.");
    }
}
