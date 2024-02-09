using Updater.Downloading.Abstract.StateMachine;

namespace Updater.Downloading.StateMachine;

internal class DownloadingStateTransition : StateTransition<DownloadingState, IDownloadingStateStrategyFactory>, IDownloadingStateTransition
{
    public DownloadingStateTransition(Lazy<IDownloadingStateStrategyFactory> stateStrategyLazyFactory)
        : base(stateStrategyLazyFactory)
    {
    }
}