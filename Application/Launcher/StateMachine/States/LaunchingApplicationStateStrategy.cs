using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine.States;

internal class LaunchingApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationStateTransition _applicationStateTransition;

    public LaunchingApplicationStateStrategy(IApplicationStateTransition applicationStateTransition)
    {
        _applicationStateTransition = applicationStateTransition;
    }

    public override ApplicationState State => ApplicationState.Launching;

    protected override Task DoEnterAsync()
    {
        Console.WriteLine("Launch %MAINPROCESS%");

        _applicationStateTransition.MoveTo(ApplicationState.Shutdown);

        return Task.CompletedTask;
    }
}