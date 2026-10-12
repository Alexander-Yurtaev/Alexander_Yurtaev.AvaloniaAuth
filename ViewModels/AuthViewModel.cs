using CommunityToolkit.Mvvm.ComponentModel;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class AuthViewModel : ObservableObject
{
    [ObservableProperty]
    private string _content = "ФгерView1Model";
}
