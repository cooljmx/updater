namespace Installer.Downloading.StateMachine;

internal interface IDownloadingContextProvider
{
    TValue GetValue<TValue>(string key)
        where TValue : notnull;
}