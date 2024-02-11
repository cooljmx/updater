namespace Launcher.Abstraction.StateMachine;

public interface IThreadPool
{
    void ExecuteAsync(Func<Task> asyncAction, CancellationToken cancellationToken);

    void ExecuteAsync(Action action, CancellationToken cancellationToken);
}