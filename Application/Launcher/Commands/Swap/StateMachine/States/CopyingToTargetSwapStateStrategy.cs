using Launcher.Abstraction.StateMachine;
using Launcher.Metadata;

namespace Launcher.Commands.Swap.StateMachine.States;

internal class CopyingToTargetSwapStateStrategy : StateStrategy<SwapState>, ISwapStateStrategy
{
    private readonly ILocalMetadataProvider _localMetadataProvider;
    private readonly ISwapContext _swapContext;
    private readonly ISwapStateTransition _swapStateTransition;

    public CopyingToTargetSwapStateStrategy(
        ISwapContext swapContext,
        ILocalMetadataProvider localMetadataProvider,
        ISwapStateTransition swapStateTransition)
    {
        _swapContext = swapContext;
        _localMetadataProvider = localMetadataProvider;
        _swapStateTransition = swapStateTransition;
    }

    public override SwapState State => SwapState.CopyingToTarget;

    protected override async Task DoEnterAsync()
    {
        var currentDirectory = AppContext.BaseDirectory;
        var metadata = await _localMetadataProvider.GetAsync(Path.Combine(currentDirectory, "metadata.json"));

        FileDeepCopy("metadata.json", currentDirectory, _swapContext.TargetPath);

        foreach (var metadataDto in metadata)
        {
            var relativeFileName = metadataDto.Url;

            FileDeepCopy(relativeFileName, currentDirectory, _swapContext.TargetPath);
        }

        _swapStateTransition.MoveTo(SwapState.OriginalLauncherStarting);
    }

    private static void FileDeepCopy(string fileName, string sourceDirectory, string targetDirectory)
    {
        var sourceFileName = Path.Combine(sourceDirectory, fileName);
        var targetFileName = Path.Combine(targetDirectory, fileName);

        var targetFileDirectory = Path.GetDirectoryName(targetFileName);

        if (!Directory.Exists(targetFileDirectory) && targetFileDirectory is not null)
            Directory.CreateDirectory(targetFileDirectory);

        File.Copy(sourceFileName, targetFileName, true);
    }
}