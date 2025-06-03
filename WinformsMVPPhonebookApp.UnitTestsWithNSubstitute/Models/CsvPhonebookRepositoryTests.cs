using WinformsMVPPhonebookApp.Models;
using NSubstitute;
using System.Xml.Linq;

namespace WinformsMVPPhonebookApp.UnitTests.Models
{
    [TestFixture]
    public class CsvPhonebookRepositoryTests
    {
        [Test]
        public void GetAllEntries_WhenFileExists_ReturnsEntries()
        {
            // Arrange
            var stubFileSystem = Substitute.For<IFileSystem>();
            stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
            string fileContent = "John Doe,123456789\r\nJane Smith,987654321";
            stubFileSystem.OpenRead(Arg.Any<string>()).Returns(
                new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));

            var repository = new CsvPhonebookRepository(stubFileSystem, "fake.csv");

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
        public void GetAllEntries_WhenFileExistsWithMissingValues_ThrowsInvalidOperationException(string name, string phoneNumber)
        {
            // Arrange
            var stubFileSystem = Substitute.For<IFileSystem>();
            stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
            string fileContent = $"John Doe,123456789\r\n{name},{phoneNumber}\r\nJane Smith,987654321";
            stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));

            var repository = new CsvPhonebookRepository(stubFileSystem, "fake.csv");

            // Act + Assert
            Assert.Throws<InvalidOperationException>(() => repository.GetAllEntries());
        }

        [Test]
        public void GetAllEntries_WhenFileExistsWithEmptyLines_ThrowsInvalidOperationException()
        {
            // Arrange
            var stubFileSystem = Substitute.For<IFileSystem>();
            stubFileSystem.FileExists(Arg.Any<string>()).Returns(true);
            string fileContent = $"John Doe,123456789\r\n\r\nJane Smith,987654321";
            stubFileSystem.OpenRead(Arg.Any<string>()).Returns(_ => new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent)));

            var repository = new CsvPhonebookRepository(stubFileSystem, "fake.csv");

            // Assert
            Assert.Throws<InvalidOperationException>(() => repository.GetAllEntries());
        }

        [Test]
        public void GetAllEntries_WhenFileDoesNotExists_ReturnEmptyList()
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
        public void GetAllEntries_WhenFileDoesNotExists_CreatesFile()
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
