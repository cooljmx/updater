using Installer.Downloading.Abstract.StateMachine;

namespace Installer.Downloading.StateMachine;

internal class DownloadingStateStrategyFactory : ApplicationStateStrategyFactory<DownloadingState>, IDownloadingStateStrategyFactory
{
    public DownloadingStateStrategyFactory(IEnumerable<Func<IDownloadingStateStrategy>> strategyFactories)
        : base(strategyFactories)
    {
    }
}