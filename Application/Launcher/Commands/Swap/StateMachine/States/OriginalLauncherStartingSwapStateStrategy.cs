using System.Diagnostics;
using Launcher.Abstraction.StateMachine;

namespace Launcher.Commands.Swap.StateMachine.States;

internal class OriginalLauncherStartingSwapStateStrategy : StateStrategy<SwapState>, ISwapStateStrategy
{
    private readonly ISwapContext _swapContext;
    private readonly ISwapStateTransition _swapStateTransition;

    public OriginalLauncherStartingSwapStateStrategy(
        ISwapContext swapContext,
        ISwapStateTransition swapStateTransition)
    {
        _swapContext = swapContext;
        _swapStateTransition = swapStateTransition;
    }

    public override SwapState State => SwapState.OriginalLauncherStarting;

    protected override Task DoEnterAsync()
    {
        var launcherFileName = Path.Combine(_swapContext.TargetPath, "launcher.exe");

        Process.Start(launcherFileName);

        _swapStateTransition.MoveTo(SwapState.Completed);

        return Task.CompletedTask;
    }
}