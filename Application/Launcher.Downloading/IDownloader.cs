namespace Launcher.Downloading;

public interface IDownloader
{
    Task DownloadAsync(Uri source, string targetPath, string checkSum);
}