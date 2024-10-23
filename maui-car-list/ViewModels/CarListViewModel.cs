using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_car_list.Models;
using maui_car_list.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace maui_car_list.ViewModels;
public partial class CarListViewModel : BaseViewModel
{
    public ObservableCollection<Car> Cars { get; private set; } = []; // private set because only set here!! Also default initialized (shorthad of new ()

    public CarListViewModel()
    {
        Title = "Car List";
    }

    [ObservableProperty]
    public bool isRefreshing;


    [RelayCommand]
    async Task GetCarListAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            if (Cars.Any()) Cars.Clear();

            var cars = App.CarService.GetCars();
            // foreach cause no AddRange for observablecollecrion
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get cars: {ex.Message}");  // sink is Output Window, when in Debug Mode
            await Shell.Current.DisplayAlert($"Error", "Failed to retrieve list of cars", "Ok");  // could abstract that away and user via DI
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }


    [RelayCommand]
    async Task GetCarDetails(Car car)
    {
        if (car == null)
        {
            await Shell.Current.DisplayAlert($"", "Not details for the car", "Ok");
            return;
        }

        await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object>
        {
            {nameof(Car), car }
        });
    }
}
