using Launcher.Abstraction.StateMachine;
using Launcher.StateMachine;

namespace Launcher;

internal sealed class Application :
    StateMachineAsyncRunner<ApplicationState, IApplicationStateMachine>,
    IApplication
{
    public Application(IApplicationStateMachine applicationStateMachine)
        : base(applicationStateMachine)
    {
    }
}