namespace Launcher.Abstraction.StateMachine;

public interface IStateStrategy<out TState>
    where TState : notnull
{
    TState State { get; }

    Task EnterAsync();

    Task ExitAsync();
}