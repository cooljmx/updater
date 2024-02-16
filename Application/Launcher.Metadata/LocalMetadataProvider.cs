using System.Text.Json;

namespace Launcher.Metadata;

internal class LocalMetadataProvider : ILocalMetadataProvider
{
    public async Task<MetadataDto[]> GetAsync(string fileName)
    {
        await using var fileStream = File.OpenRead(fileName);
        var metadataCollectionDto = await JsonSerializer.DeserializeAsync<MetadataCollectionDto>(fileStream);

        return metadataCollectionDto is null
            ? throw new InvalidOperationException()
            : metadataCollectionDto.Data;
    }
}