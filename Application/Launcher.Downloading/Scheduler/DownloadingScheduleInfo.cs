namespace Launcher.Downloading.Scheduler;

internal record DownloadingScheduleInfo(
    Uri Source,
    string TargetFileName,
    string CheckSum,
    IScheduledDownloadingSource ScheduledDownloadingSource);