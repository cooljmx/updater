using Launcher.Abstraction.StateMachine;
using Launcher.Metadata;

namespace Launcher.StateMachine.States;

internal class CopyingToTargetApplicationStateStrategy : StateStrategy<ApplicationState>, IApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IApplicationStateTransition _applicationStateTransition;
    private readonly ILocalMetadataProvider _localMetadataProvider;

    public CopyingToTargetApplicationStateStrategy(
        IApplicationContext applicationContext,
        ILocalMetadataProvider localMetadataProvider,
        IApplicationStateTransition applicationStateTransition)
    {
        _applicationContext = applicationContext;
        _localMetadataProvider = localMetadataProvider;
        _applicationStateTransition = applicationStateTransition;
    }

    public override ApplicationState State => ApplicationState.CopyingToTarget;

    protected override async Task DoEnterAsync()
    {
        var currentDirectory = AppContext.BaseDirectory;
        var metadata = await _localMetadataProvider.GetAsync(Path.Combine(currentDirectory, "metadata.json"));
        var targetPath = _applicationContext.GetValue<string>("targetPath");

        FileDeepCopy("metadata.json", currentDirectory, targetPath);

        foreach (var metadataDto in metadata)
        {
            var relativeFileName = metadataDto.Url;

            FileDeepCopy(relativeFileName, currentDirectory, targetPath);
        }

        _applicationStateTransition.MoveTo(ApplicationState.OriginalLauncherStarting);
    }

    private static void FileDeepCopy(string fileName, string sourceDirectory, string targetDirectory)
    {
        var sourceFileName = Path.Combine(sourceDirectory, fileName);
        var targetFileName = Path.Combine(targetDirectory, fileName);

        Console.WriteLine($"source: {sourceFileName}");
        Console.WriteLine($"target: {targetFileName}");

        var targetFileDirectory = Path.GetDirectoryName(targetFileName);

        if (!Directory.Exists(targetFileDirectory) && targetFileDirectory is not null)
            Directory.CreateDirectory(targetFileDirectory);

        File.Copy(sourceFileName, targetFileName, true);
    }
}