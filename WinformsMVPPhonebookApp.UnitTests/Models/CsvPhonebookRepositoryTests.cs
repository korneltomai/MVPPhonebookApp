using System.IO;
using System.Text;
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
        public void GetAllEntries_WhenFileExistsAndIsEmpty_ReturnsEmptyList()
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
        public void GetAllEntries_WhenFileExistsWithMissingValues_ThrowsInvalidOperationException(string name, string phoneNumber)
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
        public void GetAllEntries_WhenFileExistsWithEmptyLines_ThrowsInvalidOperationException()
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
        public void GetAllEntries_WhenFileDoesNotExist_ReturnEmptyList()
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
        public void GetAllEntries_WhenFileDoesNotExist_CreatesFile()
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

        [Test]
        public void DeleteEntry_WhenEntryExists_DeletesEntry()
        {
            // Arrange
            var stubFileSystem = new FakeFileSystem
            {
                DoesFileExists = true,
                FileContent = "John Doe,123456789\r\nJane Smith,987654321"
            };
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
        public void DeleteEntry_WhenFileDoesNotExist_ThrowsFileNotFoundException()
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
            return new WriteBackStream(content =>
            {
                FileContent = content;
            });
        }
    }

    public class WriteBackStream : MemoryStream
    {
        private readonly Action<string> _onDispose;

        public WriteBackStream(Action<string> onDispose) : base()
        {
            _onDispose = onDispose;
        }

        protected override void Dispose(bool disposing)
        {
            Position = 0;
            using var reader = new StreamReader(this, leaveOpen: true);
            var content = reader.ReadToEnd();
            _onDispose(content);
            base.Dispose(disposing);
        }
    }
}
