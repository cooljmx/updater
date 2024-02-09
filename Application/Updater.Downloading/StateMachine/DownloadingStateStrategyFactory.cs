using Updater.Downloading.Abstract.StateMachine;

namespace Updater.Downloading.StateMachine;

internal class DownloadingStateStrategyFactory : ApplicationStateStrategyFactory<DownloadingState>, IDownloadingStateStrategyFactory
{
    public DownloadingStateStrategyFactory(IEnumerable<Func<IDownloadingStateStrategy>> strategyFactories)
        : base(strategyFactories)
    {
    }
}