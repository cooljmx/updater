namespace Launcher.StateMachine;

internal interface IApplicationStateStrategy
{
    ApplicationState State { get; }

    void Enter();

    void Exit();
}