using Autofac;
using Nutri.Domain.Service;
using Nutri.Wpf.Service;
using Nutri.Wpf.View;
using Nutri.Wpf.ViewModel;

namespace Nutri.Wpf;

internal static class Bootstrapper
{
    private static IContainer? _container;

    public static T Resolve<T>() => _container!.Resolve<T>();

    internal static void Start()
    {
        ContainerBuilder builder = new();

        builder.RegisterUi();
        builder.RegisterServices();
        builder.RegisterStuff();

        _container = builder.Build();
    }

    internal static void Stop() => _container?.Dispose();

    private static ContainerBuilder RegisterStuff(this ContainerBuilder builder)
    {
        return builder;
    }

    private static ContainerBuilder RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<ProductService>().SingleInstance();
        builder.RegisterType<NrvService>().SingleInstance();
        builder.RegisterType<UserService>().SingleInstance();
        builder.RegisterType<GraphService>().SingleInstance();

        return builder;
    }

    private static ContainerBuilder RegisterUi(this ContainerBuilder builder)
    {
        builder.RegisterType<MainWindow>().SingleInstance();

        builder.RegisterType<ProductsView>().SingleInstance();
        builder.RegisterType<ProductsViewModel>().SingleInstance();

        return builder;
    }
}