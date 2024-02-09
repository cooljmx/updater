namespace Updater.Downloading;

internal readonly record struct DownloadFileMetadata(long FinalSize, long DownloadedSize);