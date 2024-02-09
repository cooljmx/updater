using Updater.Downloading.Abstract;
using Updater.Downloading.Abstract.StateMachine;

namespace Updater.Downloading.StateMachine;

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