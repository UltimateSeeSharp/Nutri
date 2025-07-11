﻿using Autofac;
using Nutri.Domain.Service;
using Nutri.Wpf.Service;
using Nutri.Wpf.View;
using Nutri.Wpf.ViewModel;
using Nutri.Wpf.Model;
using Microsoft.Extensions.Configuration;

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
        builder.Config();

        _container = builder.Build();
    }

    internal static void Stop() => _container?.Dispose();

    private static ContainerBuilder Config(this ContainerBuilder builder)
    {
        var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

        var config = configBuilder.Build();

        builder.Register(x =>
        {
            var c = new AppSettings();
            config.GetSection(nameof(AppSettings)).Bind(c);
            return c;
        });
        builder.RegisterInstance(config).AsImplementedInterfaces();

        return builder;
    }

    private static ContainerBuilder RegisterStuff(this ContainerBuilder builder)
    {
        return builder;
    }

    private static ContainerBuilder RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<ProductService>().SingleInstance();
        builder.RegisterType<NrvService>().SingleInstance();
        builder.RegisterType<UserService>().SingleInstance();
        builder.RegisterType<DayDistributionService>().SingleInstance();

        builder.RegisterType<GraphService>().SingleInstance();
        builder.RegisterType<FoodPortionService>().SingleInstance();

        return builder;
    }

    private static ContainerBuilder RegisterUi(this ContainerBuilder builder)
    {
        builder.RegisterType<MainWindow>().SingleInstance();

        builder.RegisterType<ProductsView>().SingleInstance();
        builder.RegisterType<ProductsViewModel>().SingleInstance();

        builder.RegisterType<DayDistributionView>().SingleInstance();
        builder.RegisterType<DayDistributionViewModel>().SingleInstance();

        builder.RegisterType<NutrientDistributionView>().SingleInstance();
        builder.RegisterType<NutrientDistributionViewModel>().SingleInstance();

        builder.RegisterType<StatisticsView>().SingleInstance();
        builder.RegisterType<StatisticsViewModel>().SingleInstance();

        builder.RegisterType<RegisterUserView>().SingleInstance();
        builder.RegisterType<RegisterUserViewModel>().SingleInstance();

        return builder;
    }
}