using System.Text.Json;
using Launcher.Abstraction.StateMachine;

namespace Launcher.Metadata.Indexer.StateMachine.States;

internal class MetadataSavingStateStrategy : StateStrategy<IndexingState>, IIndexingStateStrategy
{
    private const string MetadataRelativeFileName = "metadata.json";
    private readonly IIndexingContext _indexingContext;
    private readonly IIndexingStateTransition _indexingStateTransition;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true,
    };

    public MetadataSavingStateStrategy(
        IIndexingContext indexingContext,
        IIndexingStateTransition indexingStateTransition)
    {
        _indexingContext = indexingContext;
        _indexingStateTransition = indexingStateTransition;
    }

    public override IndexingState State => IndexingState.MetadataSaving;

    protected override async Task DoEnterAsync()
    {
        var data = _indexingContext.CheckSums
            .Where(pair => pair.Key != MetadataRelativeFileName)
            .Select(pair => new MetadataDto(pair.Key, pair.Value))
            .ToArray();

        var metadataCollectionDto = new MetadataCollectionDto(data);

        var metadataFileName = Path.Combine(_indexingContext.RootFolder, MetadataRelativeFileName);
        await using var fileStream = File.OpenWrite(metadataFileName);

        await JsonSerializer.SerializeAsync(fileStream, metadataCollectionDto, _jsonSerializerOptions);

        _indexingStateTransition.MoveTo(IndexingState.Shutdown);
    }
}