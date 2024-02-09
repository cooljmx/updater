namespace Launcher.Downloading.StateMachine;

internal enum DownloadingState
{
    ContextInitializing,
    CheckSumValidating,
    Starting,
    Continue,
    Completed,
}