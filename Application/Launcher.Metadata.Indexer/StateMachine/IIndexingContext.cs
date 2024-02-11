namespace Launcher.Metadata.Indexer.StateMachine;

public interface IIndexingContext
{
    IDictionary<string, string> CheckSums { get; }
    string RootFolder { get; set; }
    CancellationToken ShutdownCancellationToken { get; }

    Task ShutdownAsync();
}