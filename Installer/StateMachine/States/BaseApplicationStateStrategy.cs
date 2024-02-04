namespace Installer.StateMachine.States;

internal abstract class BaseApplicationStateStrategy : IApplicationStateStrategy
{
    public abstract ApplicationState State { get; }

    public void Enter()
    {
        DoEnter();
    }

    public void Exit()
    {
        DoExit();
    }

    protected virtual void DoEnter()
    {
    }

    protected virtual void DoExit()
    {
    }
}