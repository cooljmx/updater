using Launcher.Downloading.Abstract;
using Launcher.Downloading.Abstract.StateMachine;

namespace Launcher.Downloading.StateMachine;

internal class DownloadingStateMachine : StateMachine<DownloadingState>, IDownloadingStateMachine
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