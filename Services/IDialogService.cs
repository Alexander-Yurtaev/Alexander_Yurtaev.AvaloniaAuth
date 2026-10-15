using Alexander_Yurtaev.AvaloniaAuth.Dto;
using Avalonia.Controls;
using System.Threading.Tasks;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public interface IDialogService
{
    Task<RegisteredUserDto?> ShowRegisterDialog(Window owner);
    void CloseCurrentDialog(object? dialogResult);
}
