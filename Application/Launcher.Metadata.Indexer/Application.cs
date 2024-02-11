using Launcher.Abstraction.StateMachine;
using Launcher.Metadata.Indexer.StateMachine;

namespace Launcher.Metadata.Indexer;

internal sealed class Application :
    StateMachineAsyncRunner<IndexingState, IIndexingStateMachine>,
    IApplication
{
    public Application(IIndexingStateMachine indexingStateMachine)
        : base(indexingStateMachine)
    {
    }
}