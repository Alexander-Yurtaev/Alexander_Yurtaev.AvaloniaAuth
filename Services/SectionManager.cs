using Alexander_Yurtaev.AvaloniaAuth.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alexander_Yurtaev.AvaloniaAuth.Services;

public partial class SectionManager : ObservableObject, ISectionManager
{
    private readonly Dictionary<string, SectionViewModel> _sections = [];
    private readonly Dictionary<string, ObservableObject> _viewModels = [];

    [ObservableProperty]
    private SectionViewModel[] _sectionsSource = [];

    [ObservableProperty]
    private ObservableObject? _viewModel;

    [ObservableProperty]
    private SectionViewModel? _selectedSection;

    public void Add(string key, string title, Func<string, ObservableObject> viewModelFactory)
    {
        var section = new SectionViewModel(key, title, this);

        _sections.Add(key, section);
        _viewModels.Add(key, viewModelFactory(key));

        SectionsSource = _sections.Values.ToArray();
        if (SectionsSource.Any() && SelectedSection is null)
        {
            SelectedSection = SectionsSource.FirstOrDefault();
        }
    }

    partial void OnSelectedSectionChanged(SectionViewModel? value)
    {
        if (value is null ||
            !_sections.TryGetValue(value.Key, out var section) ||
            !_viewModels.TryGetValue(value.Key, out var viewModel))
        {
            ViewModel = null;
            return;
        }

        section.IsChecked = true;
        ViewModel = viewModel;
    }
}