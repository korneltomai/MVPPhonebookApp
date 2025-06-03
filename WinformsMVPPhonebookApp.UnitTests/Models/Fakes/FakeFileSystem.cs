using WinformsMVPPhonebookApp.Models;
using WinformsMVPPhonebookApp.UnitTests.Models.Helpers;

namespace WinformsMVPPhonebookApp.UnitTests.Models.Fakes
{
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
}
