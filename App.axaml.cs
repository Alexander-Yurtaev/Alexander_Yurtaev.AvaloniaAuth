using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using Alexander_Yurtaev.AvaloniaAuth.ViewModels;
using Alexander_Yurtaev.AvaloniaAuth.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;

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
        var collection = new ServiceCollection();

        collection.AddSingleton<IAuthService, AuthService>();
        collection.AddSingleton<IDialogService, DialogService>();
        collection.AddSingleton<ISectionManager, SectionManager>();

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
        collection.AddTransient<Func<RegisterDialog>>(provider =>
        {
            return provider.GetRequiredService<RegisterDialog>;
        });

        collection.AddSingleton<MainViewModel>();
        collection.AddSingleton<MainWindow>();
        collection.AddTransient<Func<MainWindow>>(provider =>
        {
            return provider.GetRequiredService<MainWindow>;
        });

        Services = collection.BuildServiceProvider();

        var sectionManager = Services.GetService<ISectionManager>()!;
        sectionManager.Add("View1", new SectionViewModel("View1", "View1", sectionManager), GetRecipientViewModel);
        sectionManager.Add("View2", new SectionViewModel("View2", "View2", sectionManager), GetViewModel);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Services.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private ObservableObject GetViewModel(IServiceProvider sp, string key)
    {
        var viewModel = sp.GetRequiredKeyedService<ObservableObject>(key);
        return viewModel;
    }

    private ObservableRecipient GetRecipientViewModel(IServiceProvider sp, string key)
    {
        var viewModel = sp.GetRequiredKeyedService<ObservableRecipient>(key);
        return viewModel;
    }
}