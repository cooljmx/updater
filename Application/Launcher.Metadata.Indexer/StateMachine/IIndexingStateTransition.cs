using Launcher.Abstraction.StateMachine;

namespace Launcher.Metadata.Indexer.StateMachine;

internal interface IIndexingStateTransition : IStateTransition<IndexingState, IIndexingStateStrategy>
{
}