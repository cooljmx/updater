using System.Text.Json.Serialization;

namespace WebInstaller;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(VersionDto))]
internal partial class VersionDtoContext : JsonSerializerContext
{
}