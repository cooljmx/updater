using Installer.Downloading.Abstract.StateMachine;

namespace Installer.Downloading.StateMachine;

internal interface IDownloadingStateStrategy : IStateStrategy<DownloadingState>
{
}