using Launcher.Downloading.Abstract;
using Launcher.Downloading.Abstract.StateMachine;

namespace Launcher.Downloading.StateMachine.States;

internal class CompletedDownloadingStateStrategy : StateStrategy<DownloadingState>, IDownloadingStateStrategy
{
    private readonly IDownloadingContextProvider _downloadingContextProvider;
    private readonly IScopeRepository _scopeRepository;

    public CompletedDownloadingStateStrategy(
        IScopeRepository scopeRepository,
        IDownloadingContextProvider downloadingContextProvider)
    {
        _scopeRepository = scopeRepository;
        _downloadingContextProvider = downloadingContextProvider;
    }

    public override DownloadingState State => DownloadingState.Completed;

    protected override Task DoEnterAsync()
    {
        var metadataFileName = _downloadingContextProvider.GetValue<string>("metadataFileName");

        if (File.Exists(metadataFileName))
            File.Delete(metadataFileName);

        var lifetimeScopeId = _downloadingContextProvider.GetValue<Guid>("lifetimeScopeId");
        _scopeRepository.Remove(lifetimeScopeId);

        return Task.CompletedTask;
    }
}