using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Updater;
using Updater.Downloading.Scheduler;

var builder = Host.CreateApplicationBuilder(args);

builder.ConfigureContainer(
    new AutofacServiceProviderFactory(
        containerBuilder =>
        {
            containerBuilder.RegisterModule<AutofacModule>();
            containerBuilder.RegisterModule<Updater.Downloading.AutofacModule>();
        }));

builder.Services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<IDownloadingBackgroundService>());
builder.Services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<IApplication>());
builder.Services.AddHttpClient();

var host = builder.Build();

await host.RunAsync();