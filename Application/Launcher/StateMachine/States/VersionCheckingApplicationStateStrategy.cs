using Launcher.Abstraction.StateMachine;
using Launcher.Versioning;

namespace Launcher.StateMachine.States;

internal class VersionCheckingApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly ILocalVersionProvider _localVersionProvider;
    private readonly IRemoteVersionProvider _remoteVersionProvider;

    public VersionCheckingApplicationStateStrategy(
        IRemoteVersionProvider remoteVersionProvider,
        ILocalVersionProvider localVersionProvider,
        IApplicationStateTransition applicationStateTransition,
        IApplicationContext applicationContext)
    {
        _remoteVersionProvider = remoteVersionProvider;
        _localVersionProvider = localVersionProvider;
        _applicationStateTransition = applicationStateTransition;
        _applicationContext = applicationContext;
    }

    public override ApplicationState State => ApplicationState.VersionChecking;

    protected override async Task DoEnterAsync()
    {
        var (version, id) = await _remoteVersionProvider.GetAsync();

        var currentVersion = _localVersionProvider.Get();

        if (version > currentVersion)
        {
            _applicationContext.SetValue("updateId", id);

            _applicationStateTransition.MoveTo(ApplicationState.MetadataPreparing);
        }
        else
        {
            _applicationStateTransition.MoveTo(ApplicationState.Launching);
        }
    }
}