using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class LoginViewModel : ObservableValidator
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;
    private readonly Func<MainWindow> _mainWindowFactory;

    public LoginViewModel(
        IAuthService authService, 
        IDialogService dialogService,
        Func<MainWindow> mainWindowFactory)
    {
        _authService = authService;
        _dialogService = dialogService;
        _mainWindowFactory = mainWindowFactory;
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Введите Email.")]
    [EmailAddress(ErrorMessage = "Введите валидный Email.")]
    private string _email = string.Empty;

    partial void OnEmailChanged(string value)
    {
        ValidateProperty(value, nameof(Email));
        LoginCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Введите пароль.")]
    [MinLength(length: 8, ErrorMessage = "Минимум 8 символов")]
    private string _password = string.Empty;

    public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    partial void OnPasswordChanged(string value)
    {
        ValidateProperty(value, nameof(Password));
        LoginCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanLogin))]
    public async Task Login()
    {
        ValidateAllProperties();
        if (HasErrors)
            return;

        try
        {
            await _authService.LoginAsync(Email, Password, CancellationToken.None);
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ErrorMessage = ex.Message;
            OnPropertyChanged(nameof(HasErrorMessage));
        }
    }

    private bool CanLogin() => !HasErrors;

    [RelayCommand(CanExecute = nameof(CanRegister))]
    public async Task Register()
    {
        try
        {
            var owner = _mainWindowFactory();
            var user = await _dialogService.ShowRegisterDialog(owner);
            if (user is not null)
            {
                Email = user.Email;
                Password = user.Password;
                await LoginCommand.ExecuteAsync(null);
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ErrorMessage = ex.Message;
            OnPropertyChanged(nameof(HasErrorMessage));
        }
    }

    private bool CanRegister() => !LoginCommand.IsRunning;
}