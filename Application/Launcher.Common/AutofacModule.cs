using Autofac;
using Launcher.Abstraction.StateMachine;
using Launcher.Common.Cryptography;
using Launcher.Common.Environment;
using Launcher.Common.Threading;

namespace Launcher.Common;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CommandLineArgumentProvider>().As<ICommandLineArgumentProvider>().SingleInstance();
        builder.RegisterType<ThreadPoolWrapper>().As<IThreadPool>().SingleInstance();
        builder.RegisterType<CheckSumCalculator>().As<ICheckSumCalculator>().SingleInstance();
    }
}