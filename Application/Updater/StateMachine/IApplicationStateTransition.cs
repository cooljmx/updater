namespace Updater.StateMachine;

internal interface IApplicationStateTransition
{
    void MoveTo(ApplicationState state);

    event Action<IApplicationStateStrategy>? MovedToState;
}