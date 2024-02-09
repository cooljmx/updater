namespace Updater.StateMachine.States;

internal class CreatedApplicationStateStrategy : BaseApplicationStateStrategy
{
    public override ApplicationState State => ApplicationState.Created;
}