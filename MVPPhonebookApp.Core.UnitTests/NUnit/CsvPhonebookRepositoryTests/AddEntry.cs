﻿using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnit.CsvPhonebookRepositoryTests;

[TestFixture]
public class AddEntry
{
    [Test]
    public void WhenFileExists_AddsEntry()
    {
        // Arrange
        var stubFileSystem = new FakeFileSystem
        {
            DoesFileExists = true,
            FileContent = "John Doe,123456789\r\nJane Smith,987654321"
        };
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

    [Test]
    public void WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var stubFileSystem = new FakeFileSystem
        {
            DoesFileExists = false,
        };
        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var entryToAdd = new PhonebookEntry("Alice Johnson", "5551234567");

        // Assert + Act
        Assert.Throws<FileNotFoundException>(() => repository.AddEntry(entryToAdd), 
            "The entries file does not exist.");
    }
}
