namespace Launcher.Abstraction.StateMachine;

public abstract class StateMachineAsyncRunner<TState, TStateMachine>
    where TState : notnull
    where TStateMachine : IStateMachine<TState>
{
    private readonly TStateMachine _stateMachine;

    protected StateMachineAsyncRunner(TStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _stateMachine.Dispose();

        return Task.CompletedTask;
    }
}