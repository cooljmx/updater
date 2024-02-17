using Launcher.Abstraction.StateMachine;
using Launcher.Commands;
using Launcher.Commands.Swap;

namespace Launcher.StateMachine.States;

internal class StartedApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly ICommandProvider _commandProvider;
    private readonly ISwapHandler _swapHandler;

    public StartedApplicationStateStrategy(
        ICommandProvider commandProvider,
        IApplicationStateTransition applicationStateTransition,
        ISwapHandler swapHandler)
    {
        _commandProvider = commandProvider;
        _applicationStateTransition = applicationStateTransition;
        _swapHandler = swapHandler;
    }

    public override ApplicationState State => ApplicationState.Started;

    protected override async Task DoEnterAsync()
    {
        switch (_commandProvider.Get())
        {
            case Command.Swap:
                await _swapHandler.ExecuteAsync();
                _applicationStateTransition.MoveTo(ApplicationState.Shutdown);
                break;
            case Command.Regular:
            default:
                _applicationStateTransition.MoveTo(ApplicationState.VersionChecking);
                break;
        }
    }
}