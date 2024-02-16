namespace Launcher.Environment;

internal class FolderProvider : IFolderProvider
{
    private const string CompanyName = "CustomUpdaterCompany";
    private const string ApplicationName = "CustomUpdater";
    private readonly string _localApplicationDataFolder;

    public FolderProvider()
    {
        _localApplicationDataFolder =
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
    }

    public string GetTemporaryFolder(Guid id)
    {
        return Path.Combine(_localApplicationDataFolder, CompanyName, ApplicationName, id.ToString());
    }
}