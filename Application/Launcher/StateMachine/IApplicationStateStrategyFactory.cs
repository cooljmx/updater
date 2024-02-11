using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine;

internal interface IApplicationStateStrategyFactory : IStateStrategyFactory<ApplicationState, IApplicationStateStrategy>
{
}