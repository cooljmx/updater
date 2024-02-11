using Launcher.Abstraction.StateMachine;

namespace Launcher.Downloading.StateMachine;

internal interface IDownloadingStateStrategyFactory : IStateStrategyFactory<DownloadingState, IDownloadingStateStrategy>
{
}