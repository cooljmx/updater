namespace Launcher.StateMachine;

internal class ApplicationContext : IApplicationContext, IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly Dictionary<string, object> _values = new();

    public ApplicationContext()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        ShutdownCancellationToken = _cancellationTokenSource.Token;
    }

    public CancellationToken ShutdownCancellationToken { get; }

    public async Task ShutdownAsync()
    {
        await _cancellationTokenSource.CancelAsync();
    }

    public TValue GetValue<TValue>(string key)
        where TValue : notnull
    {
        if (!_values.TryGetValue(key, out var value))
            throw new InvalidOperationException();

        if (value is TValue castValue)
            return castValue;

        throw new InvalidOperationException();
    }

    public void SetValue<TValue>(string key, TValue value)
        where TValue : notnull
    {
        if (!_values.TryAdd(key, value))
            throw new InvalidOperationException();
    }

    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
    }
}