namespace Updater.StateMachine;

internal interface IApplicationStateStrategyFactory
{
    IApplicationStateStrategy Create(ApplicationState state);
}