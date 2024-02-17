using Launcher.Abstraction.StateMachine;
using Launcher.Common.Environment;

namespace Launcher.Commands.Swap.StateMachine.States;

internal class ContextPreparingSwapStateStrategy : StateStrategy<SwapState>, ISwapStateStrategy
{
    private readonly ICommandLineArgumentProvider _commandLineArgumentProvider;
    private readonly ISwapStateTransition _swapStateTransition;
    private readonly IWriteableSwapContext _writeableSwapContext;

    public ContextPreparingSwapStateStrategy(
        ICommandLineArgumentProvider commandLineArgumentProvider,
        IWriteableSwapContext writeableSwapContext,
        ISwapStateTransition swapStateTransition)
    {
        _commandLineArgumentProvider = commandLineArgumentProvider;
        _writeableSwapContext = writeableSwapContext;
        _swapStateTransition = swapStateTransition;
    }

    public override SwapState State => SwapState.ContextPreparing;

    protected override Task DoEnterAsync()
    {
        var arguments = _commandLineArgumentProvider.Get();

        if (arguments.Length < 4)
            throw new InvalidOperationException();

        var processIdValue = arguments[2];
        var targetPath = arguments[3];

        if (!Directory.Exists(targetPath))
            throw new InvalidOperationException();

        if (!int.TryParse(processIdValue, out var processId))
            throw new InvalidOperationException();

        _writeableSwapContext.ProcessId = processId;
        _writeableSwapContext.TargetPath = targetPath;

        _swapStateTransition.MoveTo(SwapState.WaitingProcessFinished);

        return Task.CompletedTask;
    }
}