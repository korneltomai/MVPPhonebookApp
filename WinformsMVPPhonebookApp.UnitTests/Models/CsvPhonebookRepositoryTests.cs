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

        [Test]
        public void GetAllEntries_WhenFileExistsWithMissingValues_ReturnsEntries()
        {
            // Arrange
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = true,
                FileContent = ",123456789\nJane Smith,\n,"
            };
            var repository = new CsvPhonebookRepository(stubFileSystem, "fakePath.csv");

            // Act
            var entries = repository.GetAllEntries().ToList();

            // Assert
            Assert.That(entries.Count, Is.EqualTo(3));
            Assert.That(entries[0].Name, Is.EqualTo(""));
            Assert.That(entries[0].PhoneNumber, Is.EqualTo("123456789"));
            Assert.That(entries[1].Name, Is.EqualTo("Jane Smith"));
            Assert.That(entries[1].PhoneNumber, Is.EqualTo(""));
            Assert.That(entries[2].Name, Is.EqualTo(""));
            Assert.That(entries[2].PhoneNumber, Is.EqualTo(""));
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
            public bool DoesFileExists { get; set; }
            public string FileContent { get; set; } = string.Empty;

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
