using Launcher.Abstraction.StateMachine;

namespace Launcher.Metadata.Indexer.StateMachine;

internal class IndexingStateStrategyFactory : StateStrategyFactory<IndexingState, IIndexingStateStrategy>, IIndexingStateStrategyFactory
{
    public IndexingStateStrategyFactory(IEnumerable<Func<IIndexingStateStrategy>> strategyFactories)
        : base(strategyFactories)
    {
    }
}