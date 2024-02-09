namespace Updater.Metadata;

public interface IMetadataProvider
{
    MetadataDto[] Get(string fileName);
}