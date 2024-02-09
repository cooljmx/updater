using Launcher.Environment;

namespace Launcher.Commands;

internal class CommandProvider : ICommandProvider
{
    private readonly ICommandLineArgumentProvider _commandLineArgumentProvider;

    public CommandProvider(ICommandLineArgumentProvider commandLineArgumentProvider)
    {
        _commandLineArgumentProvider = commandLineArgumentProvider;
    }

    public Command Get()
    {
        var arguments = _commandLineArgumentProvider.Get();

        if (arguments.Length < 2)
            throw new InvalidOperationException();

        var commandValue = arguments[1];

        if (string.Equals(commandValue, "swap", StringComparison.OrdinalIgnoreCase))
            return Command.Swap;

        throw new InvalidOperationException();
    }
}