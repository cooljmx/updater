using Autofac;
using Autofac.Extensions.DependencyInjection;
using Installer;
using Installer.Downloading.Scheduler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.ConfigureContainer(
    new AutofacServiceProviderFactory(
        containerBuilder =>
        {
            containerBuilder.RegisterModule<AutofacModule>();
            containerBuilder.RegisterModule<Installer.Downloading.AutofacModule>();
        }));

builder.Services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<IDownloadingBackgroundService>());
builder.Services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<IApplication>());
builder.Services.AddHttpClient();

var host = builder.Build();

await host.RunAsync();