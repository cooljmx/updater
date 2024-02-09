namespace Launcher.StateMachine;

internal enum ApplicationState
{
    Created,
    Started,
    Swap,
    WaitingProcessFinished,
    CopyingToTarget,
}