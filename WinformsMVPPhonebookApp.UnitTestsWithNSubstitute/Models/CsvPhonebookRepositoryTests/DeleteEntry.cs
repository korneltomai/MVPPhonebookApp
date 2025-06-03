using NSubstitute;
using WinformsMVPPhonebookApp.Models;
using WinformsMVPPhonebookApp.UnitTestsWithNSubstitute.Models.Helpers;

namespace WinformsMVPPhonebookApp.UnitTestsWithNSubstitute.Models.CsvPhonebookRepositoryTests
{
    public class DeleteEntry
    {
        [Test]
        public void DeleteEntry_WhenEntryExists_DeletesEntry()
        {
            // Arrange
            var stubFileSystem = Substitute.For<IFileSystem>();
            stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
            string fileContent = "John Doe,123456789\r\nJane Smith,987654321";
            stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));
            stubFileSystem.CreateFile(Arg.Any<string>()).Returns(new WriteBackStream(content => fileContent = content));

            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");
            var entryToDelete = new PhonebookEntry("John Doe", "123456789");

            // Act
            repository.DeleteEntry(entryToDelete);

            // Assert
            var entries = repository.GetAllEntries().ToList();

            Assert.That(entries, Has.Count.EqualTo(1));
            Assert.That(entries.Contains(entryToDelete), Is.False);
        }

        [Test]
        public void DeleteEntry_WhenEntryDoesNotExist_ThrowsInvalidOperationException()
        {
            var stubFileSystem = Substitute.For<IFileSystem>();
            stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
            string fileContent = "John Doe,123456789\r\nJane Smith,987654321";
            stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));

            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");
            var entryToDelete = new PhonebookEntry("Does not exist", "999999999");

            // Assert + Act
            Exception exception = Assert.Throws<InvalidOperationException>(() => repository.DeleteEntry(entryToDelete));
            Assert.That(exception.Message, Contains.Substring("The entry to delete does not exist"));
        }

        [Test]
        public void DeleteEntry_WhenFileDoesNotExist_ThrowsFileNotFoundException()
        {
            // Arrange
            var stubFileSystem = Substitute.For<IFileSystem>();
            stubFileSystem.FileExists(Arg.Any<string>()).Returns(false);

            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");
            var entryToDelete = new PhonebookEntry("Does not exist", "999999999");

            // Assert + Act
            Exception exception = Assert.Throws<FileNotFoundException>(() => repository.DeleteEntry(entryToDelete));
            Assert.That(exception.Message, Contains.Substring("The entries file does not exist"));
        }
    }
}
