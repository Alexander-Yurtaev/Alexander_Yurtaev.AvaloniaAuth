using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Views;
using Avalonia.Controls;
using System;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public class DialogService : IDialogService
{
    private readonly Func<RegisterDialog> _factory;
    private Window? _currentDialog;

    public DialogService(Func<RegisterDialog> factory)
    {
        _factory = factory;
    }

    public void CloseCurrentDialog(object? dialogResult)
    {
        if (_currentDialog is null) return;
        _currentDialog.Close(dialogResult);
        _currentDialog = null;
    }

    public async Task<UserForLogin?> ShowRegisterDialog(Window owner)
    {
        _currentDialog = _factory();
        var user = await _currentDialog.ShowDialog<UserForLogin?>(owner);
        return user;
    }
}