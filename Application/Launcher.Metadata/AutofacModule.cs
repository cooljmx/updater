using Autofac;

namespace Launcher.Metadata;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<LocalMetadataProvider>().As<ILocalMetadataProvider>().SingleInstance();
    }
}