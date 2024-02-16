using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine.States;

internal class ShutdownApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;

    public ShutdownApplicationStateStrategy(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public override ApplicationState State => ApplicationState.Shutdown;

    protected override async Task DoEnterAsync()
    {
        await _applicationContext.ShutdownAsync();
    }
}