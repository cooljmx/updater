using Launcher.Abstraction.StateMachine;

namespace Launcher.Commands.Swap.StateMachine;

internal class SwapStateStrategyFactory : StateStrategyFactory<SwapState, ISwapStateStrategy>, ISwapStateStrategyFactory
{
    public SwapStateStrategyFactory(IEnumerable<Func<ISwapStateStrategy>> strategyFactories)
        : base(strategyFactories)
    {
    }
}