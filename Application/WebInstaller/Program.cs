using System.Diagnostics;

using var singleHttpClient = new HttpClient();
singleHttpClient.BaseAddress = new Uri("http://localhost:50001/");

try
{
    await DownloadLauncherAsync(singleHttpClient);

    var applicationFolder = GetApplicationFolder();
    var launcherFileName = Path.Combine(applicationFolder, "launcher.exe");

    Process.Start(launcherFileName);
}
catch (Exception exception)
{
    Console.WriteLine($"Application failed: {exception.Message}");
    return -1;
}

return 0;

async Task DownloadLauncherAsync(HttpClient httpClient)
{
    using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "launcher.exe");
    using var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

    if (!httpResponseMessage.IsSuccessStatusCode)
        throw new InvalidOperationException("launcher not found");

    await using var stream = await httpResponseMessage.Content.ReadAsStreamAsync();

    var applicationFolder = GetApplicationFolder();

    var launcherFileName = Path.Combine(applicationFolder, "launcher.exe");
    await using var fileStream = File.Create(launcherFileName);

    await stream.CopyToAsync(fileStream);
}

string GetApplicationFolder()
{
    const string companyName = "CustomUpdaterCompany";
    const string applicationName = "CustomUpdater";
    var localApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    return Path.Combine(localApplicationDataFolder, companyName, applicationName);
}