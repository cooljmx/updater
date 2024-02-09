using Launcher.Downloading.Abstract.StateMachine;

namespace Launcher.Downloading.StateMachine;

internal class DownloadingStateStrategyFactory : ApplicationStateStrategyFactory<DownloadingState>, IDownloadingStateStrategyFactory
{
    public DownloadingStateStrategyFactory(IEnumerable<Func<IDownloadingStateStrategy>> strategyFactories)
        : base(strategyFactories)
    {
    }
}