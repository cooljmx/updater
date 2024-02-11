namespace Launcher.Abstraction.StateMachine;

public interface IStateTransition<in TState, out TStateStrategy>
    where TState : notnull
    where TStateStrategy : IStateStrategy<TState>
{
    void MoveTo(TState state);

    event Action<TStateStrategy>? MovedToState;
}