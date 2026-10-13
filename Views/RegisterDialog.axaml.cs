using Alexander_Yurtaev.AvaloniaAuth.ViewModels;
using Avalonia.Controls;

namespace Alexander_Yurtaev.AvaloniaAuth.Views;

public partial class RegisterDialog : Window
{
    public RegisterDialog(RegisterDialogViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}