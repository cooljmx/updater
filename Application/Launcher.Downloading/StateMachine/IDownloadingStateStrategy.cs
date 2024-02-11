using Launcher.Abstraction.StateMachine;

namespace Launcher.Downloading.StateMachine;

internal interface IDownloadingStateStrategy : IStateStrategy<DownloadingState>
{
}