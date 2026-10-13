using Alexander_Yurtaev.AvaloniaAuth.Services;
using Alexander_Yurtaev.AvaloniaAuth.ViewModels;
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

        collection.AddTransient<View1Model>();
        collection.AddTransient<View2Model>();
        collection.AddTransient<AuthViewModel>();
        collection.AddTransient<LoginViewModel>();
        collection.AddTransient<AuthorizedViewModel>();

        collection.AddTransient<MainViewModel>();
        collection.AddTransient<MainWindow>();

        Services = collection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Services.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}