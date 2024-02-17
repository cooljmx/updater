namespace Launcher.Commands.Swap.StateMachine;

internal interface ISwapContext
{
    int ProcessId { get; }
    string TargetPath { get; }
    Guid ScopeId { get; }
    TaskCompletionSource TaskCompletionSource { get; }
}