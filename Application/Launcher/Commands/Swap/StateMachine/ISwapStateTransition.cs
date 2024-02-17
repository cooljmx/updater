using Launcher.Abstraction.StateMachine;

namespace Launcher.Commands.Swap.StateMachine;

internal interface ISwapStateTransition : IStateTransition<SwapState, ISwapStateStrategy>
{
}