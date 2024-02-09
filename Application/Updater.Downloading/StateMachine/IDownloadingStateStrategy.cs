using Updater.Downloading.Abstract.StateMachine;

namespace Updater.Downloading.StateMachine;

internal interface IDownloadingStateStrategy : IStateStrategy<DownloadingState>
{
}