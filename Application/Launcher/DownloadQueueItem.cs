namespace Launcher;

internal record DownloadQueueItem(Uri Source, string TargetFileName, string CheckSum);