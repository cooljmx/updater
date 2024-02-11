using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine;

internal class ApplicationStateMachine : StateMachine<ApplicationState, IApplicationStateStrategy>, IApplicationStateMachine
{
    public ApplicationStateMachine(
        IApplicationStateTransition applicationStateTransition,
        IApplicationStateStrategyFactory applicationStateStrategyFactory,
        IThreadPool threadPool)
        : base(
            ApplicationState.Created,
            applicationStateTransition,
            applicationStateStrategyFactory,
            threadPool)
    {
    }
}