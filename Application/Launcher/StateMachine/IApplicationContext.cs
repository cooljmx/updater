namespace Launcher.StateMachine;

public interface IApplicationContext
{
    CancellationToken ShutdownCancellationToken { get; }

    Task ShutdownAsync();

    TValue GetValue<TValue>(string key)
        where TValue : notnull;

    void SetValue<TValue>(string key, TValue value)
        where TValue : notnull;
}