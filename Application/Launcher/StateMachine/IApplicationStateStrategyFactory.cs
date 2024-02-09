namespace Launcher.StateMachine;

internal interface IApplicationStateStrategyFactory
{
    IApplicationStateStrategy Create(ApplicationState state);
}