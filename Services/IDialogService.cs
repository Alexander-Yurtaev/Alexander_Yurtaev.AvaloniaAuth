using Alexander_Yurtaev.AvaloniaAuth.Models;
using Avalonia.Controls;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public interface IDialogService
{
    Task<UserForLogin?> ShowRegisterDialog(Window owner);
    void CloseCurrentDialog(object? dialogResult);
}
