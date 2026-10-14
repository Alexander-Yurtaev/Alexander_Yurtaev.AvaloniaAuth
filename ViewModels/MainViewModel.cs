using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel(
        IAuthService authService,
        ISectionManager sectionManager,
        AuthViewModel authVm)
    {
        AuthViewModel = authVm;
        SectionManager = sectionManager;
        SectionManager.SelectedSection = SectionManager.SectionsSource.FirstOrDefault()!;
    }

    [ObservableProperty]
    private ObservableObject? _authViewModel;

    [ObservableProperty]
    private ISectionManager _sectionManager;
}
