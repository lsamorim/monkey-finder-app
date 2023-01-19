using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using MonkeyFinder.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MonkeyFinder.ViewModel;

[QueryProperty(nameof(Monkey), "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    private Monkey monkey;

    private readonly IMap _map;
    
    public MonkeyDetailsViewModel(IMap map)
    {
        _map = map;
    }

    [RelayCommand]
    private async Task OpenMapAsync()
    {
        try
        {
            await _map.OpenAsync(monkey.Latitude, monkey.Longitude, 
                new MapLaunchOptions
                { 
                    Name = monkey.Name,
                    NavigationMode = NavigationMode.None
                });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to open map: {ex.Message}", "Ok");
        }
    }
}