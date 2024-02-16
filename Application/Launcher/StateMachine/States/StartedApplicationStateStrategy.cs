using Launcher.Abstraction.StateMachine;
using Launcher.Commands;

namespace Launcher.StateMachine.States;

internal class StartedApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly ICommandProvider _commandProvider;

    public StartedApplicationStateStrategy(
        ICommandProvider commandProvider,
        IApplicationStateTransition applicationStateTransition)
    {
        _commandProvider = commandProvider;
        _applicationStateTransition = applicationStateTransition;
    }

    public override ApplicationState State => ApplicationState.Started;

    protected override Task DoEnterAsync()
    {
        switch (_commandProvider.Get())
        {
            case Command.Swap:
                _applicationStateTransition.MoveTo(ApplicationState.Swap);
                break;
            case Command.Regular:
            default:
                _applicationStateTransition.MoveTo(ApplicationState.VersionChecking);
                break;
        }

        return Task.CompletedTask;
    }
}