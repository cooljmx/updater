namespace Installer.Metadata;

public interface IMetadataProvider
{
    MetadataDto[] Get(string fileName);
}