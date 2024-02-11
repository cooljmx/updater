namespace Launcher.Abstraction.StateMachine;

public interface IStateStrategyFactory<in TState, out TStateStrategy>
    where TState : notnull
    where TStateStrategy : IStateStrategy<TState>
{
    TStateStrategy Create(TState state);
}