namespace Launcher.StateMachine;

internal enum ApplicationState
{
    Started,

    Swap,
    WaitingProcessFinished,
    CopyingToTarget,
    OriginalLauncherStarting,

    VersionChecking,

    MetadataPreparing,
    DownloadPreparing,
    DownloadContinuing,
    SwapStarting,

    Launching,
    Shutdown,
}