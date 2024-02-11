using Autofac;
using Launcher.Metadata.Indexer.StateMachine;
using Launcher.Metadata.Indexer.StateMachine.States;

namespace Launcher.Metadata.Indexer;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Application>().As<IApplication>().SingleInstance();

        builder.RegisterType<IndexingStateStrategyFactory>().As<IIndexingStateStrategyFactory>().SingleInstance();
        builder.RegisterType<IndexingStateTransition>().As<IIndexingStateTransition>().SingleInstance();
        builder.RegisterType<IndexingStateMachine>().As<IIndexingStateMachine>().SingleInstance().ExternallyOwned();
        builder.RegisterType<IndexingContext>().As<IIndexingContext>().SingleInstance();

        builder.RegisterType<FileCollectionBuildingStateStrategy>().As<IIndexingStateStrategy>().InstancePerDependency();
        builder.RegisterType<CheckSumCalculatingStateStrategy>().As<IIndexingStateStrategy>().InstancePerDependency();
        builder.RegisterType<MetadataSavingStateStrategy>().As<IIndexingStateStrategy>().InstancePerDependency();
        builder.RegisterType<ShutdownStateStrategy>().As<IIndexingStateStrategy>().InstancePerDependency();
    }
}