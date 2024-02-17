using Launcher.Abstraction.StateMachine;
using Launcher.Common.Scope;

namespace Launcher.Commands.Swap.StateMachine.States;

internal class CompletedSwapStateStrategy : StateStrategy<SwapState>, ISwapStateStrategy
{
    private readonly IScopeRepository _scopeRepository;
    private readonly ISwapContext _swapContext;

    public CompletedSwapStateStrategy(
        IScopeRepository scopeRepository,
        ISwapContext swapContext)
    {
        _scopeRepository = scopeRepository;
        _swapContext = swapContext;
    }

    public override SwapState State => SwapState.Completed;

    protected override Task DoEnterAsync()
    {
        _scopeRepository.Remove(_swapContext.ScopeId);

        _swapContext.TaskCompletionSource.SetResult();

        return Task.CompletedTask;
    }
}