using Launcher.Abstraction.StateMachine;

namespace Launcher.Downloading.StateMachine;

internal class DownloadingStateStrategyFactory : StateStrategyFactory<DownloadingState, IDownloadingStateStrategy>, IDownloadingStateStrategyFactory
{
    public DownloadingStateStrategyFactory(IEnumerable<Func<IDownloadingStateStrategy>> strategyFactories)
        : base(strategyFactories)
    {
    }
}