using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine;

internal class ApplicationStateMachine :
    StateMachine<ApplicationState, IApplicationStateStrategy, IApplicationStateTransition, IApplicationStateStrategyFactory>, IApplicationStateMachine
{
    public ApplicationStateMachine(
        IApplicationStateTransition applicationStateTransition,
        IApplicationStateStrategyFactory applicationStateStrategyFactory,
        IThreadPool threadPool)
        : base(
            ApplicationState.Started,
            applicationStateTransition,
            applicationStateStrategyFactory,
            threadPool)
    {
    }
}