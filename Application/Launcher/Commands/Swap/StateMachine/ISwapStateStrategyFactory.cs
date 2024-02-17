using Launcher.Abstraction.StateMachine;

namespace Launcher.Commands.Swap.StateMachine;

internal interface ISwapStateStrategyFactory : IStateStrategyFactory<SwapState, ISwapStateStrategy>
{
}