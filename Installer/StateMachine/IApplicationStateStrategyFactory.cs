namespace Installer.StateMachine;

internal interface IApplicationStateStrategyFactory
{
    IApplicationStateStrategy Create(ApplicationState state);
}