using Alexander_Yurtaev.AvaloniaAuth.ViewModels;
using Avalonia.Controls;

namespace Alexander_Yurtaev.AvaloniaAuth;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public MainWindow(MainViewModel vm) : this()
    {
        DataContext = vm;
    }
}