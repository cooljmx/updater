using Launcher.Abstraction.StateMachine;

namespace Launcher.StateMachine;

internal class ApplicationStateStrategyFactory : StateStrategyFactory<ApplicationState, IApplicationStateStrategy>, IApplicationStateStrategyFactory
{
    public ApplicationStateStrategyFactory(IEnumerable<Func<IApplicationStateStrategy>> strategyFactories)
        : base(strategyFactories)
    {
    }
}