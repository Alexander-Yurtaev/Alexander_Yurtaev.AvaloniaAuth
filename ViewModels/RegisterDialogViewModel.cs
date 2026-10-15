using Alexander_Yurtaev.AvaloniaAuth.Dto;
using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class RegisterDialogViewModel : ObservableValidator
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;
    private CancellationTokenSource? _cts;

    public RegisterDialogViewModel(IAuthService authService, IDialogService dialogService)
    {
        _authService = authService;
        _dialogService = dialogService;
    }

    public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Введите Email.")]
    [EmailAddress(ErrorMessage = "Введите валидный Email.")]
    private string _email = string.Empty;

    partial void OnEmailChanged(string value)
    {
        ValidateProperty(value, nameof(Email));
        RegisterCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Введите Имя.")]
    private string _name = string.Empty;

    partial void OnNameChanged(string value)
    {
        ValidateProperty(value, nameof(Name));
        RegisterCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    [Required(ErrorMessage = "Введите пароль.")]
    [MinLength(length: 8, ErrorMessage = "Минимум 8 символов")]
    private string _password = string.Empty;

    partial void OnPasswordChanged(string value)
    {
        ValidateProperty(value, nameof(Password));
        RegisterCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public async Task Close()
    {
        _dialogService.CloseCurrentDialog(null);
    }

    [RelayCommand]
    public async Task Cancel()
    {
        _cts?.Cancel();
        _cts = null;
    }

    [RelayCommand(CanExecute = nameof(CanRegister))]
    public async Task Register()
    {
        try
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            ValidateAllProperties();
            if (HasErrors)
                return;

            var registeredUserDto = await _authService.RegisterAsync(Email, Name, Password, _cts.Token);
            _dialogService.CloseCurrentDialog(registeredUserDto);
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            ErrorMessage = ex.Message;
            OnPropertyChanged(nameof(HasErrorMessage));
        }
    }

    private bool CanRegister() => !HasErrors && !RegisterCommand.IsRunning;
}
