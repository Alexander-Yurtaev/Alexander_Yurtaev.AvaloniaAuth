using Alexander_Yurtaev.AvaloniaAuth.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public interface ISectionManager
{
    SectionViewModel[] SectionsSource { get; }
    SectionViewModel SelectedSection { get; set; }
    ObservableObject? ViewModel { get; }
    void Add(string key, SectionViewModel section, Func<IServiceProvider, string, ObservableObject> viewModelFactory);
}