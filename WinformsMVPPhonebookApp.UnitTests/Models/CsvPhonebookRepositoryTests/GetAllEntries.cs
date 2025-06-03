using WinformsMVPPhonebookApp.Models;
using WinformsMVPPhonebookApp.UnitTests.Models.Fakes;

namespace WinformsMVPPhonebookApp.UnitTests.Models.CsvPhonebookRepositoryTests
{
    [TestFixture]
    public class GetAllEntries
    {
        public void WhenFileExists_ReturnsEntries()
        {
            // Arrange
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = true,
                FileContent = "John Doe,123456789\r\nJane Smith,987654321"
            };
            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

            // Act
            var entries = repository.GetAllEntries().ToList();

            // Assert
            Assert.That(entries, Has.Count.EqualTo(2));
            Assert.That(entries[0].Name, Is.EqualTo("John Doe"));
            Assert.That(entries[0].PhoneNumber, Is.EqualTo("123456789"));
            Assert.That(entries[1].Name, Is.EqualTo("Jane Smith"));
            Assert.That(entries[1].PhoneNumber, Is.EqualTo("987654321"));
        }

        [Test]
        public void WhenFileExistsAndIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = true,
            };
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
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = true,
                FileContent = "John Doe,123456789\r\n" +
                              $"{name},{phoneNumber}\r\n" +
                              "Jane Smith,987654321"
            };
            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

            // Assert
            Exception exception = Assert.Throws<InvalidOperationException>(() => repository.GetAllEntries());
            Assert.That(exception.Message, Contains.Substring("Each line in the entries file must contain exactly two values"));
        }

        [Test]
        public void WhenFileExistsWithEmptyLines_ThrowsInvalidOperationException()
        {
            // Arrange
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = true,
                FileContent = "John Doe,123456789\r\n" +
                              $"\r\n" +
                              "Jane Smith,987654321"
            };
            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

            // Assert
            Exception exception = Assert.Throws<InvalidOperationException>(() => repository.GetAllEntries());
            Assert.That(exception.Message, Contains.Substring("The entries file must not contain empty lines"));
        }

        [Test]
        public void WhenFileDoesNotExist_ReturnEmptyList()
        {
            // Arrange
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = false
            };
            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

            // Act
            var entries = repository.GetAllEntries().ToList();

            // Assert
            Assert.That(entries, Is.Empty);
        }

        [Test]
        public void WhenFileDoesNotExist_CreatesFile()
        {
            // Arrange
            var mockFileSystem = new FakeFileSystem
            {
                DoesFileExists = false
            };
            var repository = new CsvPhonebookRepository(mockFileSystem, "fakePath.csv");

            // Act
            var _ = repository.GetAllEntries();

            // Assert
            Assert.That(mockFileSystem.CreateFileCalled, Is.True);
        }
    }
}
