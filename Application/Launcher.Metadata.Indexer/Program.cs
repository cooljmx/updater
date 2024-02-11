using Autofac;
using Autofac.Extensions.DependencyInjection;
using Launcher.Metadata.Indexer;
using Launcher.Metadata.Indexer.StateMachine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.ConfigureContainer(
    new AutofacServiceProviderFactory(
        containerBuilder =>
        {
            containerBuilder.RegisterModule<AutofacModule>();
            containerBuilder.RegisterModule<Launcher.Metadata.AutofacModule>();
            containerBuilder.RegisterModule<Launcher.Common.AutofacModule>();
        }));

builder.Services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<IApplication>());

var host = builder.Build();

var indexingContext = host.Services.GetRequiredService<IIndexingContext>();

await host.RunAsync(indexingContext.ShutdownCancellationToken);