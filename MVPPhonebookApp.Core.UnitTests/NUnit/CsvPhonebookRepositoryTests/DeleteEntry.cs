﻿using MVPPhonebookApp.Core.Models;
using MVPPhonebookApp.Core.Repository;
using MVPPhonebookApp.Core.UnitTests.Fakes;

namespace MVPPhonebookApp.Core.UnitTests.NUnit.CsvPhonebookRepositoryTests;

[TestFixture]
public class DeleteEntry
{
    [Test]
    public void WhenFileExists_DeletesEntry()
    {
        // Arrange
        var stubFileSystem = new FakeFileSystem
        {
            DoesFileExists = true,
            FileContent = "John Doe,123456789\r\nJane Smith,987654321"
        };
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
    public void WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var stubFileSystem = new FakeFileSystem
        {
            DoesFileExists = false,
        };
        var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

        var entryToDelete = new PhonebookEntry("Does not exist", "999999999");

        // Assert + Act
        Assert.Throws<FileNotFoundException>(() => repository.DeleteEntry(entryToDelete), 
            "The entries file does not exist.");
    }
}
