namespace Launcher.StateMachine;

internal enum ApplicationState
{
    Started,
    Swap,
    WaitingProcessFinished,
    CopyingToTarget,
    VersionChecking,
    MetadataPreparing,
    DownloadPreparing,
    DownloadContinuing,
    Launching,
}