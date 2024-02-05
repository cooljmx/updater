using Installer.Environment;

namespace Installer.StateMachine.States;

internal class SwapApplicationStateStrategy : BaseApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly ICommandLineArgumentProvider _commandLineArgumentProvider;

    public SwapApplicationStateStrategy(
        ICommandLineArgumentProvider commandLineArgumentProvider,
        IApplicationContext applicationContext,
        IApplicationStateTransition applicationStateTransition)
    {
        _commandLineArgumentProvider = commandLineArgumentProvider;
        _applicationContext = applicationContext;
        _applicationStateTransition = applicationStateTransition;
    }

    public override ApplicationState State => ApplicationState.Swap;

    protected override void DoEnter()
    {
        var arguments = _commandLineArgumentProvider.Get();

        if (arguments.Length < 4)
            throw new InvalidOperationException();

        var processIdValue = arguments[2];
        var targetPath = arguments[3];

        if (!Directory.Exists(targetPath))
            throw new InvalidOperationException();

        if (!int.TryParse(processIdValue, out var processId))
            throw new InvalidOperationException();

        _applicationContext.SetValue("processId", processId);
        _applicationContext.SetValue("targetPath", targetPath);

        _applicationStateTransition.MoveTo(ApplicationState.WaitingProcessFinished);
    }
}