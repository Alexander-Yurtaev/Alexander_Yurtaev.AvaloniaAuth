using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly AuthViewModel _authVm;

    private readonly Dictionary<Enums.SelectedViewModel, ObservableObject> _viewModels = [];

    [ObservableProperty]
    private ObservableObject? _authViewModel;

    [ObservableProperty]
    private ObservableObject? _viewModel;

    [ObservableProperty]
    private Enums.SelectedViewModel _selectedViewModel;

    public MainViewModel(
        IAuthService authService,
        View1Model view1Model,
        View2Model view2Model,
        AuthViewModel authVm)
    {
        _authService = authService;
        _viewModels.Add(Enums.SelectedViewModel.View1Model, view1Model);
        _viewModels.Add(Enums.SelectedViewModel.View2Model, view2Model);
        _authVm = authVm;

        AuthViewModel = _authVm;
        this.SelectedViewModel = Enums.SelectedViewModel.View1Model;
    }

    partial void OnSelectedViewModelChanged(Enums.SelectedViewModel value)
    {
        this.ViewModel = _viewModels[value];
    }
}
