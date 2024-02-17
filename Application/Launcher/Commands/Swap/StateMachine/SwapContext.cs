namespace Launcher.Commands.Swap.StateMachine;

internal class SwapContext : IWriteableSwapContext, ISwapContext
{
    private string? _targetPath;
    private TaskCompletionSource? _taskCompletionSource;

    public int ProcessId { get; set; }

    public string TargetPath
    {
        get => _targetPath
               ?? throw new InvalidOperationException();
        set => _targetPath = value;
    }

    public Guid ScopeId { get; set; }

    public TaskCompletionSource TaskCompletionSource
    {
        get => _taskCompletionSource
               ?? throw new InvalidOperationException();
        set => _taskCompletionSource = value;
    }
}