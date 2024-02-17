using System.Diagnostics;
using Launcher.Abstraction.StateMachine;

namespace Launcher.Commands.Swap.StateMachine.States;

internal class WaitingProcessFinishedSwapStateStrategy : StateStrategy<SwapState>, ISwapStateStrategy
{
    private readonly ISwapContext _swapContext;
    private readonly ISwapStateTransition _swapStateTransition;

    public WaitingProcessFinishedSwapStateStrategy(
        ISwapContext swapContext,
        ISwapStateTransition swapStateTransition)
    {
        _swapContext = swapContext;
        _swapStateTransition = swapStateTransition;
    }

    public override SwapState State => SwapState.WaitingProcessFinished;

    protected override async Task DoEnterAsync()
    {
        var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        try
        {
            var process = Process.GetProcessById(_swapContext.ProcessId);

            await process.WaitForExitAsync(cancellationTokenSource.Token);
        }
        catch (ArgumentException)
        {
        }
        finally
        {
            await cancellationTokenSource.CancelAsync();
            cancellationTokenSource.Dispose();
        }

        _swapStateTransition.MoveTo(SwapState.CopyingToTarget);
    }
}