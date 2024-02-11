namespace Launcher.Abstraction.StateMachine;

public interface IStateMachine<TState> : IDisposable
    where TState : notnull
{
}