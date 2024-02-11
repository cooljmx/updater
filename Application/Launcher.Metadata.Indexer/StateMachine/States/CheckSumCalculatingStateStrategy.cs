using Launcher.Abstraction.StateMachine;
using Launcher.Common.Cryptography;
using Microsoft.Extensions.Logging;

namespace Launcher.Metadata.Indexer.StateMachine.States;

internal class CheckSumCalculatingStateStrategy : StateStrategy<IndexingState>, IIndexingStateStrategy
{
    private readonly ICheckSumCalculator _checkSumCalculator;
    private readonly IIndexingContext _indexingContext;
    private readonly IIndexingStateTransition _indexingStateTransition;
    private readonly ILogger _logger;

    public CheckSumCalculatingStateStrategy(
        IIndexingContext indexingContext,
        ICheckSumCalculator checkSumCalculator,
        IIndexingStateTransition indexingStateTransition,
        ILogger<CheckSumCalculatingStateStrategy> logger)
    {
        _indexingContext = indexingContext;
        _checkSumCalculator = checkSumCalculator;
        _indexingStateTransition = indexingStateTransition;
        _logger = logger;
    }

    public override IndexingState State => IndexingState.CheckSumCalculating;

    protected override async Task DoEnterAsync()
    {
        var checkSumTasks = new Dictionary<string, Task<string>>();

        foreach (var (relativeFileName, _) in _indexingContext.CheckSums)
        {
            var fileName = Path.Combine(_indexingContext.RootFolder, relativeFileName);

            checkSumTasks.Add(relativeFileName, CalculateAsync(fileName));
        }

        await Task.WhenAll(checkSumTasks.Values);

        foreach (var checkSumTask in checkSumTasks)
        {
            var value = await checkSumTask.Value;

            _indexingContext.CheckSums[checkSumTask.Key] = value;
        }

        _indexingStateTransition.MoveTo(IndexingState.MetadataSaving);
    }

    private Task<string> CalculateAsync(string fileName)
    {
        var checkSum = _checkSumCalculator.CalculateAsync(fileName);

        checkSum.ContinueWith(_ => _logger.LogInformation($"Check sum calculated for {fileName}"));

        return checkSum;
    }
}