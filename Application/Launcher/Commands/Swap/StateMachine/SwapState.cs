namespace Launcher.Commands.Swap.StateMachine;

public enum SwapState
{
    ContextPreparing,
    WaitingProcessFinished,
    CopyingToTarget,
    OriginalLauncherStarting,
    Completed,
}