namespace Launcher.Versioning;

internal interface IRemoteVersionProvider
{
    Task<VersionDto> GetAsync();
}