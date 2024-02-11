using Launcher.Abstraction.StateMachine;

namespace Launcher.Metadata.Indexer.StateMachine;

internal class IndexingStateTransition : StateTransition<IndexingState, IIndexingStateStrategy, IIndexingStateStrategyFactory>,
    IIndexingStateTransition
{
    public IndexingStateTransition(Lazy<IIndexingStateStrategyFactory> stateStrategyLazyFactory)
        : base(stateStrategyLazyFactory)
    {
    }
}