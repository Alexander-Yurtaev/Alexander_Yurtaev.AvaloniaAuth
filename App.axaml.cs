using Alexander_Yurtaev.AvaloniaAuth.Services;
using Alexander_Yurtaev.AvaloniaAuth.ViewModels;
using Alexander_Yurtaev.AvaloniaAuth.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Alexander_Yurtaev.AvaloniaAuth;

public partial class App : Application
{
    public static IServiceProvider? Services {  get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();

        collection.AddSingleton<IAuthService, AuthService>();
        collection.AddSingleton<IDialogService, DialogService>();

        collection.AddTransient<View1Model>();
        collection.AddTransient<View2Model>();
        collection.AddTransient<AuthViewModel>();
        collection.AddTransient<LoginViewModel>();
        collection.AddTransient<AuthorizedViewModel>();

        collection.AddTransient<RegisterDialogViewModel>();
        collection.AddTransient<RegisterDialog>();

        collection.AddSingleton<MainViewModel>();
        collection.AddSingleton<MainWindow>();

        Services = collection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Services.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}