namespace Launcher.StateMachine;

internal enum ApplicationState
{
    Started,

    VersionChecking,

    MetadataPreparing,
    DownloadPreparing,
    DownloadContinuing,
    SwapStarting,

    Launching,
    Shutdown,
}