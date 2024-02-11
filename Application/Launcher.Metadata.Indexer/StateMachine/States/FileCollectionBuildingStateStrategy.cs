using Launcher.Abstraction.StateMachine;
using Launcher.Common.Environment;

namespace Launcher.Metadata.Indexer.StateMachine.States;

internal class FileCollectionBuildingStateStrategy : StateStrategy<IndexingState>, IIndexingStateStrategy
{
    private readonly ICommandLineArgumentProvider _commandLineArgumentProvider;
    private readonly IIndexingContext _indexingContext;
    private readonly IIndexingStateTransition _indexingStateTransition;

    public FileCollectionBuildingStateStrategy(
        ICommandLineArgumentProvider commandLineArgumentProvider,
        IIndexingContext indexingContext,
        IIndexingStateTransition indexingStateTransition)
    {
        _commandLineArgumentProvider = commandLineArgumentProvider;
        _indexingContext = indexingContext;
        _indexingStateTransition = indexingStateTransition;
    }

    public override IndexingState State => IndexingState.FileCollectionBuilding;

    protected override Task DoEnterAsync()
    {
        var arguments = _commandLineArgumentProvider.Get();

        if (arguments.Length < 2)
            throw new InvalidOperationException();

        var folder = arguments[1];

        if (!Directory.Exists(folder))
            throw new InvalidOperationException();

        _indexingContext.RootFolder = folder;

        Explore(folder);

        _indexingStateTransition.MoveTo(IndexingState.CheckSumCalculating);

        return Task.CompletedTask;
    }

    private void Explore(string rootPath)
    {
        var directories = Directory.GetDirectories(rootPath);

        foreach (var path in directories)
            Explore(path);

        var fileNames = Directory.GetFiles(rootPath);

        foreach (var fileName in fileNames)
        {
            var relativePath = Path.GetRelativePath(_indexingContext.RootFolder, fileName);

            _indexingContext.CheckSums.Add(relativePath, string.Empty);
        }
    }
}