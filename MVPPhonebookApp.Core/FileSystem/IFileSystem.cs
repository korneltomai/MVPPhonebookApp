namespace MVPPhonebookApp.Core.FileSystem;

public interface IFileSystem
{
    bool FileExists(string path);
    Stream OpenRead(string path);
    Stream CreateFile(string path);
}
