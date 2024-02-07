namespace Installer.Downloading.Abstract.StateMachine;

public abstract class StateStrategy<TState> : IStateStrategy<TState>
    where TState : notnull
{
    public abstract TState State { get; }

    public async Task EnterAsync()
    {
        await DoEnterAsync();
    }

    public async Task ExitAsync()
    {
        await DoExitAsync();
    }

    protected virtual Task DoEnterAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual Task DoExitAsync()
    {
        return Task.CompletedTask;
    }
}