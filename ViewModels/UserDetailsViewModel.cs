using Alexander_Yurtaev.AvaloniaAuth.Helpers;
using Alexander_Yurtaev.AvaloniaAuth.Models;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class UserDetailsViewModel : ObservableObject
{
    private const string DefaultAvatar = "avares://Alexander_Yurtaev.AvaloniaAuth/Assets/AvatarBase.png";

    public UserDetailsViewModel(UserDetails details)
    {
        Model = details;
        var _ = LoadAvatarCommand.ExecuteAsync(null);
    }

    public UserDetails Model { get; set; }

    [ObservableProperty]
    private Bitmap? _avatar;

    [RelayCommand]
    public async Task LoadAvatar()
    {
        try
        {
            var bitmap = await ImageHelper.LoadFromWeb(new Uri(Model.Avatar));
            Avatar = bitmap ?? new Bitmap(AssetLoader.Open(new Uri(DefaultAvatar)));
        }
        catch
        {
            Avatar = new Bitmap(AssetLoader.Open(new Uri(DefaultAvatar)));
        }
    }
}
