namespace Installer.Commands;

internal interface ICommandProvider
{
    Command Get();
}