using Installer.Downloading.Abstract.StateMachine;

namespace Installer.Downloading.StateMachine;

internal class DownloadingStateTransition : StateTransition<DownloadingState, IDownloadingStateStrategyFactory>, IDownloadingStateTransition
{
    public DownloadingStateTransition(Lazy<IDownloadingStateStrategyFactory> stateStrategyLazyFactory)
        : base(stateStrategyLazyFactory)
    {
    }
}