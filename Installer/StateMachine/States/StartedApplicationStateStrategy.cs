using Installer.Commands;

namespace Installer.StateMachine.States;

internal class StartedApplicationStateStrategy : BaseApplicationStateStrategy
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

    protected override void DoEnter()
    {
        switch (_commandProvider.Get())
        {
            case Command.Swap:
                _applicationStateTransition.MoveTo(ApplicationState.Swap);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}