using Alexander_Yurtaev.AvaloniaAuth.ViewModels;
using Avalonia.Controls;

namespace Alexander_Yurtaev.AvaloniaAuth.Views;

public partial class RegisterDialog : Window
{
    public RegisterDialog()
    {
        InitializeComponent();
    }

    public RegisterDialog(RegisterDialogViewModel vm) : this()
    {
        DataContext = vm;
    }
}