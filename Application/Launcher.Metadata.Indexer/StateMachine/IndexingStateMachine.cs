using Launcher.Abstraction.StateMachine;

namespace Launcher.Metadata.Indexer.StateMachine;

internal class IndexingStateMachine : StateMachine<IndexingState, IIndexingStateStrategy>, IIndexingStateMachine
{
    public IndexingStateMachine(
        IIndexingStateTransition stateTransition,
        IIndexingStateStrategyFactory stateStrategyFactory,
        IThreadPool threadPool)
        : base(
            IndexingState.FileCollectionBuilding,
            stateTransition,
            stateStrategyFactory,
            threadPool)
    {
    }
}