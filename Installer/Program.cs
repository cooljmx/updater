//using System.Text.Json;
//using Installer;
//using Microsoft.Extensions.Hosting;

//var versionDto = new VersionDto(
//    new Version(1, 0, 0, 0),
//    new[]
//    {
//        new FileInfoDto("e:\\temp\\upd_folder\\1.0.0.1\\Installer.exe", "")
//    });

//var versionValue = JsonSerializer.Serialize(versionDto);

//File.WriteAllText("e:\\temp\\upd_folder\\version.json", versionValue);

//using System.Reflection;

//var assembly = typeof(Program).Assembly;

//var customAttribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();


//Console.WriteLine(customAttribute.Version);

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Installer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.ConfigureContainer(new AutofacServiceProviderFactory(containerBuilder =>
{
    containerBuilder.RegisterModule<AutofacModule>();
}));

var host = builder.Build();

var application = host.Services.GetRequiredService<IApplication>();

application.Start();