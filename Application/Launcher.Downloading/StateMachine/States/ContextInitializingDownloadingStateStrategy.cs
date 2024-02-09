using Launcher.Downloading.Abstract.StateMachine;

namespace Launcher.Downloading.StateMachine.States;

internal class ContextInitializingDownloadingStateStrategy : StateStrategy<DownloadingState>, IDownloadingStateStrategy
{
    private readonly IDownloadingContextProvider _downloadingContextProvider;
    private readonly IDownloadingContextUpdater _downloadingContextUpdater;
    private readonly IDownloadingStateTransition _downloadingStateTransition;

    public ContextInitializingDownloadingStateStrategy(
        IDownloadingStateTransition downloadingStateTransition,
        IDownloadingContextProvider downloadingContextProvider,
        IDownloadingContextUpdater downloadingContextUpdater)
    {
        _downloadingStateTransition = downloadingStateTransition;
        _downloadingContextProvider = downloadingContextProvider;
        _downloadingContextUpdater = downloadingContextUpdater;
    }

    public override DownloadingState State => DownloadingState.ContextInitializing;

    protected override Task DoEnterAsync()
    {
        var targetPath = _downloadingContextProvider.GetValue<string>("targetPath");

        var targetFolder = Path.GetDirectoryName(targetPath)
                           ?? throw new InvalidOperationException();

        var targetFileName = Path.GetFileName(targetPath)
                             ?? throw new InvalidOperationException();

        var metadataFileName = Path.Combine(targetFolder, $"{targetFileName}.download.metadata");
        var downloadFileName = Path.Combine(targetFolder, $"{targetFileName}.download");

        _downloadingContextUpdater.SetValue("metadataFileName", metadataFileName);
        _downloadingContextUpdater.SetValue("downloadFileName", downloadFileName);

        _downloadingStateTransition.MoveTo(DownloadingState.CheckSumValidating);

        return Task.CompletedTask;
    }
}