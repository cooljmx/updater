namespace Launcher.Metadata;

public interface ILocalMetadataProvider
{
    Task<MetadataDto[]> GetAsync(string fileName);
}