namespace Launcher.Downloading.Abstract.StateMachine;

public interface IStateStrategyFactory<TState>
    where TState : notnull
{
    IStateStrategy<TState> Create(TState state);
}