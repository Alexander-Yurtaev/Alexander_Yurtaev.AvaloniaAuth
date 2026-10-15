using Alexander_Yurtaev.AvaloniaAuth.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class View2Model : ObservableObject
{
    public View2Model()
    {
        DeveloperDetails.Add(new DeveloperDetailItem("Имя", "Александр Юртаев"));
        DeveloperDetails.Add(new DeveloperDetailItem("Роль", ".NET / WPF Developer"));
        DeveloperDetails.Add(new DeveloperDetailItem("Email", "ayurtaev.dev@mail.ru"));
        DeveloperDetails.Add(new DeveloperDetailItem("GitHub", "github.com/Alexander-Yurtaev"));
        DeveloperDetails.Add(new DeveloperDetailItem("Стек", ".NET 10, Avalonia, MVVM"));
    }

    [ObservableProperty]
    private List<DeveloperDetailItem> _developerDetails = [];
}
