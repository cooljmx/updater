using Launcher.Abstraction.StateMachine;

namespace Launcher.Metadata.Indexer.StateMachine.States;

internal class ShutdownStateStrategy : StateStrategy<IndexingState>, IIndexingStateStrategy
{
    private readonly IIndexingContext _indexingContext;

    public ShutdownStateStrategy(IIndexingContext indexingContext)
    {
        _indexingContext = indexingContext;
    }

    public override IndexingState State => IndexingState.Shutdown;

    protected override async Task DoEnterAsync()
    {
        await _indexingContext.ShutdownAsync();
    }
}