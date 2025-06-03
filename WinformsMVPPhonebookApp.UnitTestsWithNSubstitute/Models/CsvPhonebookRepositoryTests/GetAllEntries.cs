using NSubstitute;
using WinformsMVPPhonebookApp.Models;
using WinformsMVPPhonebookApp.UnitTestsWithNSubstitute.Models.Helpers;

namespace WinformsMVPPhonebookApp.UnitTestsWithNSubstitute.Models.CsvPhonebookRepositoryTests
{
    [TestFixture]
    public class GetAllEntries
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
    }
}
