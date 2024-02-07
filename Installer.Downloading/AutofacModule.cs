﻿using Autofac;
using Installer.Downloading.Abstract;
using Installer.Downloading.Scheduler;
using Installer.Downloading.StateMachine;
using Installer.Downloading.StateMachine.States;

namespace Installer.Downloading;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ThreadPoolWrapper>().As<IThreadPool>().SingleInstance();
        builder.RegisterType<ScopeRepository>().As<IScopeRepository>().SingleInstance();
        builder.RegisterType<CheckSumCalculator>().As<ICheckSumCalculator>().SingleInstance();

        builder.RegisterType<DownloadingStateStrategyFactory>().As<IDownloadingStateStrategyFactory>().InstancePerLifetimeScope();
        builder.RegisterType<DownloadingStateTransition>().As<IDownloadingStateTransition>().InstancePerLifetimeScope();
        builder.RegisterType<DownloadingStateMachine>().As<IDownloadingStateMachine>().InstancePerLifetimeScope();
        builder.RegisterType<DownloadingContext>()
            .As<IDownloadingContextProvider>()
            .As<IDownloadingContextUpdater>()
            .InstancePerLifetimeScope();

        builder.RegisterType<DownloadFileMetadataService>().As<IDownloadFileMetadataService>().InstancePerLifetimeScope();

        builder.RegisterType<Downloader>().As<IDownloader>().SingleInstance();
        builder.RegisterType<DownloadingScheduler>()
            .As<IDownloadingScheduler>()
            .As<IDownloadingBackgroundService>()
            .SingleInstance();

        builder.RegisterType<ContextInitializingDownloadingStateStrategy>().As<IDownloadingStateStrategy>().InstancePerDependency();
        builder.RegisterType<CheckSumValidatingDownloadingStateStrategy>().As<IDownloadingStateStrategy>().InstancePerDependency();
        builder.RegisterType<CompletedDownloadingStateStrategy>().As<IDownloadingStateStrategy>().InstancePerDependency();
        builder.RegisterType<StartingDownloadingStateStrategy>().As<IDownloadingStateStrategy>().InstancePerDependency();
        builder.RegisterType<ContinueDownloadingStateStrategy>().As<IDownloadingStateStrategy>().InstancePerDependency();
    }
}