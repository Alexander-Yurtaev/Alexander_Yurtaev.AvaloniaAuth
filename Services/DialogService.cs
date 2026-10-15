using Alexander_Yurtaev.AvaloniaAuth.Dto;
using Alexander_Yurtaev.AvaloniaAuth.Views;
using Avalonia.Controls;
using System;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public class DialogService : IDialogService
{
    private readonly Func<RegisterDialog> _registerDialogFactory;
    private Window? _currentDialog;

    public DialogService(Func<RegisterDialog> factory)
    {
        _registerDialogFactory = factory;
    }

    public void CloseCurrentDialog(object? dialogResult)
    {
        if (_currentDialog is null) return;
        _currentDialog.Close(dialogResult);
        _currentDialog = null;
    }

    public async Task<RegisteredUserDto?> ShowRegisterDialog(Window owner)
    {
        _currentDialog = _registerDialogFactory();
        var registeredUserDto = await _currentDialog.ShowDialog<RegisteredUserDto?>(owner);
        return registeredUserDto;
    }
}