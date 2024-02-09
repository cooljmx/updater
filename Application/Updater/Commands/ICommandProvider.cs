namespace Updater.Commands;

internal interface ICommandProvider
{
    Command Get();
}