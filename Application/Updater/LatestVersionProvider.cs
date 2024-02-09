using System.Text.Json;

namespace Updater;

public class LatestVersionProvider
{
    public async Task<VersionDto> GetAsync()
    {
        var versionValue = await File.ReadAllTextAsync("e:\\temp\\upd_folder\\version.json");

        var versionDto = JsonSerializer.Deserialize<VersionDto>(versionValue);

        if (versionDto is null)
            throw new InvalidOperationException();

        return versionDto;
    }
}