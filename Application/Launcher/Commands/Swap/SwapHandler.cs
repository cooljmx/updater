using Autofac;
using Launcher.Commands.Swap.StateMachine;
using Launcher.Common.Scope;

namespace Launcher.Commands.Swap;

internal class SwapHandler : ISwapHandler
{
    private readonly IScopeRepository _scopeRepository;

    public SwapHandler(IScopeRepository scopeRepository)
    {
        _scopeRepository = scopeRepository;
    }

    public Task ExecuteAsync()
    {
        var scopeId = Guid.NewGuid();
        var lifetimeScope = _scopeRepository.Add(scopeId);
        var writeableSwapContext = lifetimeScope.Resolve<IWriteableSwapContext>();

        writeableSwapContext.ScopeId = scopeId;
        writeableSwapContext.TaskCompletionSource = new TaskCompletionSource();

        lifetimeScope.Resolve<ISwapStateMachine>();

        return writeableSwapContext.TaskCompletionSource.Task;
    }
}