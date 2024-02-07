using Autofac;
using Autofac.Extensions.DependencyInjection;
using Installer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.ConfigureContainer(
    new AutofacServiceProviderFactory(
        containerBuilder =>
        {
            containerBuilder.RegisterModule<AutofacModule>();
        }));

var host = builder.Build();

var application = host.Services.GetRequiredService<IApplication>();

application.Start();