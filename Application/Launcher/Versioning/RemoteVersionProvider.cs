using System.Net.Http.Json;

namespace Launcher.Versioning;

internal class RemoteVersionProvider : IRemoteVersionProvider
{
    private readonly IBaseAddressProvider _baseAddressProvider;
    private readonly IHttpClientFactory _httpClientFactory;

    public RemoteVersionProvider(
        IBaseAddressProvider baseAddressProvider,
        IHttpClientFactory httpClientFactory)
    {
        _baseAddressProvider = baseAddressProvider;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<VersionDto> GetAsync()
    {
        var baseAddress = _baseAddressProvider.Get();
        using var httpClient = _httpClientFactory.CreateClient(baseAddress.AbsoluteUri);
        httpClient.BaseAddress = baseAddress;

        var versionDto = await httpClient.GetFromJsonAsync<VersionDto>("/version.json");

        if (versionDto is null)
            throw new InvalidOperationException();

        return versionDto;
    }
}