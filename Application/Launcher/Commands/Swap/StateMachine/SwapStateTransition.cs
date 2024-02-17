using Launcher.Abstraction.StateMachine;

namespace Launcher.Commands.Swap.StateMachine;

internal class SwapStateTransition : StateTransition<SwapState, ISwapStateStrategy, ISwapStateStrategyFactory>, ISwapStateTransition
{
    public SwapStateTransition(Lazy<ISwapStateStrategyFactory> stateStrategyLazyFactory)
        : base(stateStrategyLazyFactory)
    {
    }
}