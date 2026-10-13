using Alexander_Yurtaev.AvaloniaAuth.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public class AuthService : ObservableObject, IAuthService
{
    public bool IsAuthorized => User is not null;
    public User? User { get; private set; }

    public async Task LoginAsync(string email, string password)
    {
        var id = new Random().Next(100);
        var source = @"avares://Alexander.Yurtaev.Avalonia.Auth;component/Assets/AvatarBase.png";
        User = new User(id, email, $"User_{id}", source, $"Role_{id}");
        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<User?>(User));
        await Task.Delay(0);
    }

    public async Task LogoutAsync()
    {
        User = null;
        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<User?>(User));
        await Task.Delay(0);
    }

    public async Task RegisterAsync(string email, string name, string password)
    {
        var id = new Random().Next(100);
        var source = @"avares://Alexander.Yurtaev.Avalonia.Auth;component/Assets/AvatarBase.png";
        User = new User(id, email, $"User_{id}", source, $"Role_{id}");
        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<User?>(User));
        await Task.Delay(0);
    }
}