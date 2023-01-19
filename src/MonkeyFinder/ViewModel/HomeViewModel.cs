using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Networking;
using MonkeyFinder.Model;
using MonkeyFinder.Services;
using MonkeyFinder.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MonkeyFinder.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NavBarIsNotVisible))]
    private bool navBarIsVisible = true;

    public bool NavBarIsNotVisible => !NavBarIsVisible;

    [ObservableProperty]
    private bool isRefreshing;

    private readonly MonkeyService _monkeyService;

    private readonly IConnectivity _connectivity;
    private readonly IGeolocation _geoLocation;
    public ObservableCollection<Monkey> Monkeys { get; } = new ObservableCollection<Monkey>();

    public HomeViewModel(MonkeyService monkeyService,
        IConnectivity connectivity,
        IGeolocation geolocation)
    {
        Title = "Monkey Finder";
        _monkeyService = monkeyService;
        _connectivity = connectivity;
        _geoLocation = geolocation;
    }

    [RelayCommand]
    private void SwitchNavBar()
    {
        NavBarIsVisible = !NavBarIsVisible;
    }

    [RelayCommand]
    private void SwitchTheme()
    {
        Application.Current.UserAppTheme = (Application.Current.UserAppTheme == AppTheme.Light) ? AppTheme.Dark : AppTheme.Light;
    }

    [RelayCommand]
    private async Task GetMonkeysAsync()
    {
        if (IsBusy)
            return;

        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet issue", "Please, check your internet and try again", "Ok");
                return;
            }

            IsBusy = true;
            //await Task.Delay(5000);
            var monkeys = await _monkeyService.GetMonkeysAsync();

            if (Monkeys.Any())
                Monkeys.Clear();

            foreach (var monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get monkeys: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task GetClosestMonkeyAsync()
    {
        if (IsBusy || Monkeys.Count == 0)
            return;

        try
        {
            IsBusy = true;

            var location = await _geoLocation.GetLastKnownLocationAsync();

            if (location == null)
            {
                location = await _geoLocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            if (location == null)
                return;

            var closestMonkey = Monkeys.OrderBy(m => 
                location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Miles)).First();

            await Shell.Current.DisplayAlert("Closest Monkey", $"{closestMonkey.Name} {closestMonkey.Location}", "Ok");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get closest monkey: {ex.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoToDetailsAsync(Monkey monkey)
    {
        if (monkey is null) return;

        await Shell.Current.GoToAsync($"{nameof(MonkeyDetailsPage)}", true,
            new Dictionary<string, object>()
            {
                { "Monkey", monkey }
            });

    }
}
