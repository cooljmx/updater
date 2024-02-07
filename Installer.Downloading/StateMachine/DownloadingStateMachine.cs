using Installer.Downloading.Abstract;
using Installer.Downloading.Abstract.StateMachine;

namespace Installer.Downloading.StateMachine;

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