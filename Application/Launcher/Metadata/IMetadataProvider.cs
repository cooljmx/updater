namespace Launcher.Metadata;

public interface IMetadataProvider
{
    MetadataDto[] Get(string fileName);
}