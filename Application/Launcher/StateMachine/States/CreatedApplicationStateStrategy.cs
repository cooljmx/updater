using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine.States;

internal class CreatedApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    public override ApplicationState State => ApplicationState.Created;
}