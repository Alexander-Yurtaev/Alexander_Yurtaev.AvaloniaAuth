using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class View1Model : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly UnAthorizedModel _unAthorizedModel;

    public View1Model(IAuthService authService)
    {
        _authService = authService;
        _unAthorizedModel = new UnAthorizedModel();
    }

    public object? Content => _authService.IsAuthorized ? null : _unAthorizedModel;
}