using WinformsMVPPhonebookApp.Models;
using WinformsMVPPhonebookApp.UnitTests.Models.Fakes;

namespace WinformsMVPPhonebookApp.UnitTests.Models.CsvPhonebookRepositoryTests
{
    [TestFixture]
    public class DeleteEntry
    {
        [Test]
        public void WhenEntryExists_DeletesEntry()
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
            var entries = repository.GetAllEntries().ToList();

            // Assert
            Assert.That(expectedEntries.Count, Is.EqualTo(entries.Count));
            Assert.That(entries, Has.All.Matches<PhonebookEntry>(entry =>
                expectedEntries.Any(e => e.Name == entry.Name && e.PhoneNumber == entry.PhoneNumber)
            ));
        }

        [Test]
        public void WhenEntryDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = true,
                FileContent = "John Doe,123456789\r\nJane Smith,987654321"
            };
            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");
            
            var entryToDelete = new PhonebookEntry("Does not exist", "999999999");

            // Assert + Act
            Exception exception = Assert.Throws<InvalidOperationException>(() => repository.DeleteEntry(entryToDelete));
            Assert.That(exception.Message, Contains.Substring("The entry to delete does not exist"));
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
            Exception exception = Assert.Throws<FileNotFoundException>(() => repository.DeleteEntry(entryToDelete));
            Assert.That(exception.Message, Contains.Substring("The entries file does not exist"));
        }
    }
}
