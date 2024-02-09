using Updater.Metadata;

namespace Updater.StateMachine.States;

internal class CopyingToTargetApplicationStateStrategy : BaseApplicationStateStrategy
{
    private readonly IApplicationContext _applicationContext;
    private readonly IMetadataProvider _metadataProvider;

    public CopyingToTargetApplicationStateStrategy(
        IApplicationContext applicationContext,
        IMetadataProvider metadataProvider)
    {
        _applicationContext = applicationContext;
        _metadataProvider = metadataProvider;
    }

    public override ApplicationState State => ApplicationState.CopyingToTarget;

    protected override void DoEnter()
    {
        var currentDirectory = System.Environment.CurrentDirectory;
        var metadata = _metadataProvider.Get(Path.Combine(currentDirectory, "metadata.json"));
        var targetPath = _applicationContext.GetValue<string>("targetPath");

        FileDeepCopy("metadata.json", currentDirectory, targetPath);

        foreach (var metadataDto in metadata)
        {
            var relativeFileName = metadataDto.Url;

            FileDeepCopy(relativeFileName, currentDirectory, targetPath);
        }
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

        File.Copy(sourceFileName, targetFileName);
    }
}