using Alexander_Yurtaev.AvaloniaAuth.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Alexander_Yurtaev.AvaloniaAuth.Models;

public partial class SectionViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<string>>
{
    private readonly ISectionManager _sectionManager;

    public SectionViewModel(string key, string title, ISectionManager sectionManager)
    {
        IsActive = true;
        Title = title;
        _sectionManager = sectionManager;
        Key = key;
        IsChecked = false;
    }

    [ObservableProperty]
    private bool? _isChecked;

    [ObservableProperty]
    private string _key;

    [ObservableProperty]
    private string _title;
    
    partial void OnIsCheckedChanged(bool? value)
    {
        if (value == true)
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>(Key));
        }
    }

    public void Receive(ValueChangedMessage<string> message)
    {
        IsChecked = Key == message.Value;
        if (IsChecked == true)
        {
            _sectionManager.SelectedSection = this;
        }
    }
}