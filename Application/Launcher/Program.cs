using Autofac;
using Autofac.Extensions.DependencyInjection;
using Launcher;
using Launcher.StateMachine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.ConfigureContainer(
    new AutofacServiceProviderFactory(
        containerBuilder =>
        {
            containerBuilder.RegisterModule<AutofacModule>();
            containerBuilder.RegisterModule<Launcher.Downloading.AutofacModule>();
            containerBuilder.RegisterModule<Launcher.Metadata.AutofacModule>();
            containerBuilder.RegisterModule<Launcher.Common.AutofacModule>();
        }));

builder.Services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<IApplication>());
builder.Services.AddHttpClient();

var host = builder.Build();

var applicationContext = host.Services.GetRequiredService<IApplicationContext>();

await host.RunAsync(applicationContext.ShutdownCancellationToken);