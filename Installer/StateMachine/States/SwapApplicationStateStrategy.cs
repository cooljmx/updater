using System.Diagnostics;
using Installer.Environment;

namespace Installer.StateMachine.States;

internal class SwapApplicationStateStrategy : BaseApplicationStateStrategy
{
    private readonly ICommandLineArgumentProvider _commandLineArgumentProvider;

    public SwapApplicationStateStrategy(ICommandLineArgumentProvider commandLineArgumentProvider)
    {
        _commandLineArgumentProvider = commandLineArgumentProvider;
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

        try
        {
            var process = Process.GetProcessById(processId);

            process.WaitForExit(TimeSpan.FromSeconds(5));
        }
        catch (ArgumentException _)
        {
        }

        var sourceFileName = GetType().Assembly.Location;
        var targetFileName = Path.Combine(targetPath, Path.GetFileName(sourceFileName));

        File.Delete(targetFileName);
        File.Copy(sourceFileName, targetFileName);
    }
}