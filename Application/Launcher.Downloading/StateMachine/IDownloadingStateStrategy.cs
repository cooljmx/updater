using Launcher.Downloading.Abstract.StateMachine;

namespace Launcher.Downloading.StateMachine;

internal interface IDownloadingStateStrategy : IStateStrategy<DownloadingState>
{
}