using Launcher.Downloading.Abstract.StateMachine;

namespace Launcher.Downloading.StateMachine;

internal class DownloadingStateTransition : StateTransition<DownloadingState, IDownloadingStateStrategyFactory>, IDownloadingStateTransition
{
    public DownloadingStateTransition(Lazy<IDownloadingStateStrategyFactory> stateStrategyLazyFactory)
        : base(stateStrategyLazyFactory)
    {
    }
}