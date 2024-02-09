namespace Launcher.Downloading.Abstract;

internal class ThreadPoolWrapper : IThreadPool
{
    public void ExecuteAsync(Func<Task> asyncAction, CancellationToken cancellationToken)
    {
        Task.Run(asyncAction, cancellationToken);
    }

    public void ExecuteAsync(Action action, CancellationToken cancellationToken)
    {
        Task.Run(action, cancellationToken);
    }
}