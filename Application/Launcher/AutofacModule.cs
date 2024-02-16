using Autofac;
using Launcher.Commands;
using Launcher.Environment;
using Launcher.StateMachine;
using Launcher.StateMachine.States;
using Launcher.Versioning;

namespace Launcher;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<Application>().As<IApplication>().SingleInstance();
        builder.RegisterType<CommandProvider>().As<ICommandProvider>().SingleInstance();
        builder.RegisterType<LocalVersionProvider>().As<ILocalVersionProvider>().SingleInstance();
        builder.RegisterType<RemoteVersionProvider>().As<IRemoteVersionProvider>().SingleInstance();
        builder.RegisterType<BaseAddressProvider>().As<IBaseAddressProvider>().SingleInstance();
        builder.RegisterType<FolderProvider>().As<IFolderProvider>().SingleInstance();

        builder.RegisterType<ApplicationStateMachine>()
            .As<IApplicationStateMachine>()
            .SingleInstance()
            .ExternallyOwned();

        builder.RegisterType<ApplicationStateTransition>().As<IApplicationStateTransition>().SingleInstance();
        builder.RegisterType<ApplicationContext>().As<IApplicationContext>().SingleInstance();
        builder.RegisterType<ApplicationStateStrategyFactory>().As<IApplicationStateStrategyFactory>().SingleInstance();

        builder.RegisterType<StartedApplicationStateStrategy>().As<IApplicationStateStrategy>().InstancePerDependency();
        builder.RegisterType<SwapApplicationStateStrategy>().As<IApplicationStateStrategy>().InstancePerDependency();
        builder.RegisterType<WaitingProcessFinishedApplicationStateStrategy>()
            .As<IApplicationStateStrategy>()
            .InstancePerDependency();

        builder.RegisterType<CopyingToTargetApplicationStateStrategy>()
            .As<IApplicationStateStrategy>()
            .InstancePerDependency();

        builder.RegisterType<VersionCheckingApplicationStateStrategy>().As<IApplicationStateStrategy>().InstancePerDependency();
        builder.RegisterType<MetadataPreparingApplicationStateStrategy>().As<IApplicationStateStrategy>().InstancePerDependency();
        builder.RegisterType<DownloadContinuingApplicationStateStrategy>().As<IApplicationStateStrategy>().InstancePerDependency();
        builder.RegisterType<DownloadPreparingApplicationStateStrategy>().As<IApplicationStateStrategy>().InstancePerDependency();
    }
}