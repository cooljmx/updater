using Launcher.Abstraction.StateMachine;

namespace Launcher.Downloading.StateMachine;

internal class DownloadingStateMachine :
    StateMachine<DownloadingState, IDownloadingStateStrategy, IDownloadingStateTransition, IDownloadingStateStrategyFactory>, IDownloadingStateMachine
{
    public DownloadingStateMachine(
        IDownloadingStateTransition stateTransition,
        IDownloadingStateStrategyFactory stateStrategyFactory,
        IThreadPool threadPool)
        : base(
            DownloadingState.ContextInitializing,
            stateTransition,
            stateStrategyFactory,
            threadPool)
    {
    }
}