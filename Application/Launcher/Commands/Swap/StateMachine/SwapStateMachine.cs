using Launcher.Abstraction.StateMachine;

namespace Launcher.Commands.Swap.StateMachine;

internal class SwapStateMachine : StateMachine<SwapState, ISwapStateStrategy, ISwapStateTransition, ISwapStateStrategyFactory>, ISwapStateMachine
{
    public SwapStateMachine(
        ISwapStateTransition stateTransition,
        ISwapStateStrategyFactory stateStrategyFactory,
        IThreadPool threadPool)
        : base(
            SwapState.ContextPreparing,
            stateTransition,
            stateStrategyFactory,
            threadPool)
    {
    }
}