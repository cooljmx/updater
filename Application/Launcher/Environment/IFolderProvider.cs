namespace Launcher.Environment;

public interface IFolderProvider
{
    string GetTemporaryFolder(Guid id);
}