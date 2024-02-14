using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine;

internal class ApplicationStateTransition : StateTransition<ApplicationState, IApplicationStateStrategy, IApplicationStateStrategyFactory>,
    IApplicationStateTransition
{
    public ApplicationStateTransition(Lazy<IApplicationStateStrategyFactory> stateStrategyLazyFactory)
        : base(stateStrategyLazyFactory)
    {
    }
}