using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class RegisterDialogViewModel : ObservableValidator
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;

    public RegisterDialogViewModel(IAuthService authService, IDialogService dialogService)
    {
        _authService = authService;
        _dialogService = dialogService;
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Введите Email.")]
    [EmailAddress(ErrorMessage = "Введите валидный Email.")]
    private string _email;

    partial void OnEmailChanged(string value)
    {
        ValidateProperty(value, nameof(Email));
        RegisterCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Введите Имя.")]
    private string _name;

    partial void OnNameChanged(string value)
    {
        ValidateProperty(value, nameof(Name));
        RegisterCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Введите пароль.")]
    [MinLength(length: 8, ErrorMessage = "Длинна пароля должна быть не меньше 8 символов.")]
    private string _password;

    partial void OnPasswordChanged(string value)
    {
        ValidateProperty(value, nameof(Password));
        RegisterCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public async Task Cancel()
    {
        _dialogService.CloseCurrentDialog(null);
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    public async Task Register()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        var result = await _authService.RegisterAsync(Email, Name, Password);
        if (result == true)
        {
            var user = new UserForLogin(Email, Password);
            _dialogService.CloseCurrentDialog(user);
        }        
    }

    private bool CanSubmit() => !HasErrors;
}
