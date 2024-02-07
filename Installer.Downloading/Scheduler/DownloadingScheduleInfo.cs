namespace Installer.Downloading.Scheduler;

internal record DownloadingScheduleInfo(Uri Source, string TargetFileName, string CheckSum);