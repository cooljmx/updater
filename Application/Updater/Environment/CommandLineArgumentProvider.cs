namespace Updater.Environment;

internal class CommandLineArgumentProvider : ICommandLineArgumentProvider
{
    public string[] Get()
    {
        return System.Environment.GetCommandLineArgs();
    }
}