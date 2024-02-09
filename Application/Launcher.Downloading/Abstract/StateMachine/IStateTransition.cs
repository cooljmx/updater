namespace Launcher.Downloading.Abstract.StateMachine;

public interface IStateTransition<TState>
    where TState : notnull
{
    void MoveTo(TState state);

    event Action<IStateStrategy<TState>>? MovedToState;
}