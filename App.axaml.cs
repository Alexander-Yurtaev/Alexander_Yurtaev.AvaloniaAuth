using Alexander_Yurtaev.AvaloniaAuth.Data;
using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using Alexander_Yurtaev.AvaloniaAuth.Tests;
using Alexander_Yurtaev.AvaloniaAuth.ViewModels;
using Alexander_Yurtaev.AvaloniaAuth.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Alexander_Yurtaev.AvaloniaAuth;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        const bool UseFakeHandler = true;

        var collection = new ServiceCollection();

        collection.AddSingleton<IAuthService, AuthService>();
        collection.AddSingleton<IDialogService, DialogService>();
        collection.AddSingleton<ISectionManager, SectionManager>();
        collection.AddSingleton<IDataProvider, DataProvider>();
        collection.AddSingleton<FakeHttpMessageHandler>();

        var httpClientBuilder = collection.AddHttpClient("fakeapifordevs", config =>
            {
                config.BaseAddress = new Uri("https://fakeapifordevs.vercel.app");
                config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 1.0));
            });

        if (UseFakeHandler)
        {
            httpClientBuilder.ConfigurePrimaryHttpMessageHandler<FakeHttpMessageHandler>();
        }

        // Регистрируем ViewModel для разделов
        collection.AddKeyedSingleton<ObservableRecipient, View1Model>("View1");
        collection.AddKeyedSingleton<ObservableObject, View2Model>("View2");

        collection.AddTransient<AuthViewModel>();
        collection.AddTransient<AuthorizedViewModel>();
        collection.AddTransient<LoginViewModel>();
        collection.AddTransient<Func<LoginViewModel>>(provider =>
        {
            return provider.GetRequiredService<LoginViewModel>;
        });

        collection.AddTransient<RegisterDialogViewModel>();
        collection.AddTransient<RegisterDialog>();
        collection.AddTransient<Func<RegisterDialog>>(sp => sp.GetRequiredService<RegisterDialog>);

        collection.AddSingleton<MainViewModel>();
        collection.AddSingleton<MainWindow>();
        collection.AddTransient<Func<MainWindow>>(sp => sp.GetRequiredService<MainWindow>);

        Services = collection.BuildServiceProvider();

        // Регистрируем разделы
        var sectionManager = Services.GetRequiredService<ISectionManager>();
        var sp = Services;
        sectionManager.Add(key:"View1", title:"View1", sp.GetRequiredKeyedService<ObservableRecipient>);
        sectionManager.Add(key:"View2", title: "View2", sp.GetRequiredKeyedService<ObservableObject>);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Services.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}