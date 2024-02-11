using Launcher.Abstraction.StateMachine;

namespace Launcher.Metadata.Indexer.StateMachine;

internal interface IIndexingStateStrategyFactory : IStateStrategyFactory<IndexingState, IIndexingStateStrategy>
{
}