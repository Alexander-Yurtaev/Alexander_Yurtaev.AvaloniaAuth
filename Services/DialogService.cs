using Alexander_Yurtaev.AvaloniaAuth.Models;
using Alexander_Yurtaev.AvaloniaAuth.Views;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public class DialogService : IDialogService
{
    private Window? _currentDialog;

    public void CloseCurrentDialog(object? dialogResult)
    {
        if (_currentDialog is null) return;
        _currentDialog.Close(dialogResult);
        _currentDialog = null;
    }

    public async Task<UserForLogin?> ShowRegisterDialog(Window owner)
    {
        _currentDialog = App.Services!.GetRequiredService<RegisterDialog>();
        var user = await _currentDialog.ShowDialog<UserForLogin?>(owner);
        return user;
    }
}