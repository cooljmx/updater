using Launcher.Abstraction.StateMachine;

namespace Launcher.Downloading.StateMachine;

internal interface IDownloadingStateTransition : IStateTransition<DownloadingState, IDownloadingStateStrategy>
{
}