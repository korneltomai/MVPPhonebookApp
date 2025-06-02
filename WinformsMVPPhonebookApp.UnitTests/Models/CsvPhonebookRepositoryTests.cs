using WinformsMVPPhonebookApp.Models;

namespace WinformsMVPPhonebookApp.UnitTests.Models
{
    [TestFixture]
    public class CsvPhonebookRepositoryTests
    {
        [Test]
        public void GetAllEntries_WhenFileExists_ReturnsEntries()
        {
            // Arrange
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = true,
                FileContent = "John Doe,123456789\nJane Smith,987654321"
            };
            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

            // Act
            var entries = repository.GetAllEntries().ToList();

            // Assert
            Assert.That(entries.Count, Is.EqualTo(2));
            Assert.That(entries[0].Name, Is.EqualTo("John Doe"));
            Assert.That(entries[0].PhoneNumber, Is.EqualTo("123456789"));
            Assert.That(entries[1].Name, Is.EqualTo("Jane Smith"));
            Assert.That(entries[1].PhoneNumber, Is.EqualTo("987654321"));
        }

        [TestCase("", "123456789")]
        [TestCase("John Doe", "")]
        [TestCase("", "")]
        public void GetAllEntries_WhenFileExistsWithMissingValues_ThrowsException(string name, string phoneNumber)
        {
            // Arrange
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = true,
                FileContent = "John Doe,123456789\n" +
                              $"{name},{phoneNumber}\n" +
                              "Jane Smith,987654321"
            };
            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

            // Assert
            Assert.Throws<InvalidOperationException>(() => repository.GetAllEntries());
        }

        [Test]
        public void GetAllEntries_WhenFileExistsWithEmptyLines_ThrowsInvalidOperationException()
        {
            // Arrange
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = true,
                FileContent = "John Doe,123456789\n" +
                              $"\n" +
                              "Jane Smith,987654321"
            };
            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

            // Assert
            Assert.Throws<InvalidOperationException>(() => repository.GetAllEntries());
        }

        [Test]
        public void GetAllEntries_WhenFileDoesNotExists_ReturnEmptyList()
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
            Assert.That(entries.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetAllEntries_WhenFileDoesNotExists_CreatesFile()
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

        public class FakeFileSystem : IFileSystem
        {
            public bool CreateFileCalled { get; private set; } = false;
            public bool DoesFileExists;
            public string FileContent = string.Empty;

            public bool FileExists(string path) => DoesFileExists;

            public Stream OpenRead(string path) => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(FileContent));

            public Stream CreateFile(string path)
            {
                CreateFileCalled = true;
                return new MemoryStream();
            }
        }
    }
}
