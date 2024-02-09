using Autofac;
using Updater.Commands;
using Updater.Environment;
using Updater.Metadata;
using Updater.StateMachine;
using Updater.StateMachine.States;

namespace Updater;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<Application>().As<IApplication>().SingleInstance();
        builder.RegisterType<CommandLineArgumentProvider>().As<ICommandLineArgumentProvider>().SingleInstance();
        builder.RegisterType<CommandProvider>().As<ICommandProvider>().SingleInstance();
        builder.RegisterType<MetadataProvider>().As<IMetadataProvider>().SingleInstance();

        builder.RegisterType<ApplicationStateMachine>()
            .As<IApplicationStateMachine>()
            .SingleInstance()
            .ExternallyOwned();

        builder.RegisterType<ApplicationStateTransition>().As<IApplicationStateTransition>().SingleInstance();
        builder.RegisterType<ApplicationContext>().As<IApplicationContext>().SingleInstance();
        builder.RegisterType<ApplicationStateStrategyFactory>().As<IApplicationStateStrategyFactory>().SingleInstance();

        builder.RegisterType<CreatedApplicationStateStrategy>().As<IApplicationStateStrategy>().InstancePerDependency();
        builder.RegisterType<StartedApplicationStateStrategy>().As<IApplicationStateStrategy>().InstancePerDependency();
        builder.RegisterType<SwapApplicationStateStrategy>().As<IApplicationStateStrategy>().InstancePerDependency();
        builder.RegisterType<WaitingProcessFinishedApplicationStateStrategy>()
            .As<IApplicationStateStrategy>()
            .InstancePerDependency();

        builder.RegisterType<CopyingToTargetApplicationStateStrategy>()
            .As<IApplicationStateStrategy>()
            .InstancePerDependency();
    }
}