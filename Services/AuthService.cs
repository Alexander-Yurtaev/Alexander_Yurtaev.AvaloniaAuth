using Alexander_Yurtaev.AvaloniaAuth.Helpers;
using Alexander_Yurtaev.AvaloniaAuth.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public class AuthService : ObservableObject, IAuthService
{
    public bool IsAuthorized => User is not null;
    public User? User { get; private set; }

    public void Login(string email, string password)
    {
        var id = new Random().Next(100);
        var source = @"avares://Alexander.Yurtaev.Avalonia.Auth;component/Assets/AvatarBase.png";
        User = new User(id, email, $"User_{id}", source, $"Role_{id}");
        OnPropertyChanged(nameof(IsAuthorized));
    }

    public void Logout()
    {
        User = null;
        OnPropertyChanged(nameof(IsAuthorized));
    }

    public void Register(string email, string name, string password)
    {
        var id = new Random().Next(100);
        var source = @"avares://Alexander.Yurtaev.Avalonia.Auth;component/Assets/AvatarBase.png";
        User = new User(id, email, $"User_{id}", source, $"Role_{id}");
        OnPropertyChanged(nameof(IsAuthorized));
    }
}