﻿using Installer.Downloading.Abstract.StateMachine;

namespace Installer.Downloading.StateMachine.States;

internal class CheckSumValidatingDownloadingStateStrategy : StateStrategy<DownloadingState>, IDownloadingStateStrategy
{
    private readonly ICheckSumCalculator _checkSumCalculator;
    private readonly IDownloadingContextProvider _downloadingContextProvider;
    private readonly IDownloadingStateTransition _downloadingStateTransition;

    public CheckSumValidatingDownloadingStateStrategy(
        IDownloadingStateTransition downloadingStateTransition,
        ICheckSumCalculator checkSumCalculator,
        IDownloadingContextProvider downloadingContextProvider)
    {
        _downloadingStateTransition = downloadingStateTransition;
        _checkSumCalculator = checkSumCalculator;
        _downloadingContextProvider = downloadingContextProvider;
    }

    public override DownloadingState State => DownloadingState.CheckSumValidating;

    protected override async Task DoEnterAsync()
    {
        var targetPath = _downloadingContextProvider.GetValue<string>("targetPath");
        var expectedCheckSum = _downloadingContextProvider.GetValue<string>("checkSum");

        if (File.Exists(targetPath))
        {
            var checkSum = await _checkSumCalculator.CalculateAsync(targetPath);

            if (string.Equals(checkSum, expectedCheckSum, StringComparison.OrdinalIgnoreCase))
            {
                _downloadingStateTransition.MoveTo(DownloadingState.Completed);

                return;
            }

            File.Delete(targetPath);
        }

        _downloadingStateTransition.MoveTo(DownloadingState.Starting);
    }
}