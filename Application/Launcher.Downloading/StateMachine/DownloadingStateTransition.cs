using Launcher.Abstraction.StateMachine;

namespace Launcher.Downloading.StateMachine;

internal class DownloadingStateTransition : StateTransition<DownloadingState, IDownloadingStateStrategy, IDownloadingStateStrategyFactory>,
    IDownloadingStateTransition
{
    public DownloadingStateTransition(Lazy<IDownloadingStateStrategyFactory> stateStrategyLazyFactory)
        : base(stateStrategyLazyFactory)
    {
    }
}