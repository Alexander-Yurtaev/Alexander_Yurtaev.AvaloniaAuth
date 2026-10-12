using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace Alexander_Yurtaev.AvaloniaAuth.ViewModels;

public partial class View2Model : ObservableObject
{
    public View2Model()
    {
        DeveloperDetails.Add(new DeveloperDetail("Имя", "Александр"));
        DeveloperDetails.Add(new DeveloperDetail("Роль", ".NET / AvaloniaUI Developer"));
        DeveloperDetails.Add(new DeveloperDetail("Email", "ayurtaev.dev@mail.ru"));
        DeveloperDetails.Add(new DeveloperDetail("GitHub", "github.com/Alexander-Yurtaev"));
        DeveloperDetails.Add(new DeveloperDetail("Стек", ".NET 10, Avalonia, MVVM"));
    }

    [ObservableProperty]
    private List<DeveloperDetail> _developerDetails = [];
}

public record DeveloperDetail(string Name, string Value);