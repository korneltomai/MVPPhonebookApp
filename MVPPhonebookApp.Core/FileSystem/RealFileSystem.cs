namespace MVPPhonebookApp.Core.FileSystem;

public class RealFileSystem : IFileSystem
{
    public bool FileExists(string path) => File.Exists(path);
    public Stream OpenRead(string path) => File.OpenRead(path);
    public Stream CreateFile(string path) => File.Create(path);
}
