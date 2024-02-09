namespace Updater.Downloading.StateMachine;

internal class DownloadingContext : IDownloadingContextProvider, IDownloadingContextUpdater
{
    private readonly Dictionary<string, object> _values = new();

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
        _values[key] = value;
    }
}