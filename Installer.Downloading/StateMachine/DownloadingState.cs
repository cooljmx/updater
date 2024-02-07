namespace Installer.Downloading.StateMachine;

internal enum DownloadingState
{
    ContextInitializing,
    CheckSumValidating,
    Starting,
    Continue,
    Completed,
}