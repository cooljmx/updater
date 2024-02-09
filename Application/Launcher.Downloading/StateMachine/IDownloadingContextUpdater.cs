namespace Launcher.Downloading.StateMachine;

internal interface IDownloadingContextUpdater
{
    void SetValue<TValue>(string key, TValue value)
        where TValue : notnull;
}