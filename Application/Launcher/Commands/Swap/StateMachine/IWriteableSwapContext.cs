namespace Launcher.Commands.Swap.StateMachine;

internal interface IWriteableSwapContext
{
    int ProcessId { get; set; }
    string TargetPath { get; set; }
    Guid ScopeId { get; set; }
    TaskCompletionSource TaskCompletionSource { get; set; }
}