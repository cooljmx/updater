namespace Launcher.Commands;

internal interface ICommandProvider
{
    Command Get();
}