using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine;

internal interface IApplicationStateTransition : IStateTransition<ApplicationState, IApplicationStateStrategy>
{
}