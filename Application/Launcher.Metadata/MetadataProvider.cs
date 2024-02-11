using System.Text.Json;

namespace Launcher.Metadata;

internal class MetadataProvider : IMetadataProvider
{
    public MetadataDto[] Get(string fileName)
    {
        using var fileStream = File.OpenRead(fileName);
        var metadataCollectionDto = JsonSerializer.Deserialize<MetadataCollectionDto>(fileStream);

        return metadataCollectionDto is null
            ? throw new InvalidOperationException()
            : metadataCollectionDto.Data;
    }
}