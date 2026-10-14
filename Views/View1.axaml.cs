using Avalonia.Controls;
using System;

namespace Alexander_Yurtaev.AvaloniaAuth.Views;

public partial class View1 : UserControl
{
    public View1()
    {
        InitializeComponent();
    }

    protected override Type StyleKeyOverride => typeof(UserControl);
}