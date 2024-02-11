namespace Launcher.Metadata.Indexer.StateMachine;

internal class IndexingContext : IIndexingContext, IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private string? _rootFolder;

    public IndexingContext()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        ShutdownCancellationToken = _cancellationTokenSource.Token;
    }

    public IDictionary<string, string> CheckSums { get; } = new Dictionary<string, string>();

    public string RootFolder
    {
        get => _rootFolder ??
               throw new InvalidOperationException();
        set => _rootFolder = value;
    }

    public CancellationToken ShutdownCancellationToken { get; }

    public async Task ShutdownAsync()
    {
        await _cancellationTokenSource.CancelAsync();
    }

    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
    }
}