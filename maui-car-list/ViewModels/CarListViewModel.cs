using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_car_list.Models;
using maui_car_list.Services;
using maui_car_list.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace maui_car_list.ViewModels;
public partial class CarListViewModel : BaseViewModel
{
    private const string editButtonText = "Edit Car";
    private const string addButtonText = "Add Car";
    private readonly CarApiService carApiService;

    // use remote db if in internet, else use local db
    NetworkAccess accessType = Connectivity.Current.NetworkAccess;

    string message = string.Empty;

    public ObservableCollection<Car> Cars { get; private set; } = [];

    public CarListViewModel(CarApiService carApiService)
    {
        this.carApiService = carApiService;
        Title = "Car List";
        AddEditButtonText = addButtonText;
        AddEditButtonColor = null;
    }


    [ObservableProperty]
    public bool isRefreshing;
    [ObservableProperty]
    string? make;
    [ObservableProperty]
    string? model;
    [ObservableProperty]
    string? vin;
    [ObservableProperty]
    string addEditButtonText;
    [ObservableProperty]
    int carId;
    [ObservableProperty]
    Color addEditButtonColor;


    [RelayCommand]
    public async Task GetCarListAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            if (Cars.Any()) Cars.Clear();
            var cars = new List<Car>();

            if (accessType == NetworkAccess.Internet)
            {
                cars = await carApiService.GetCars();
            }
            else
            {
                cars = App.CarService.GetCars();
            }

            // foreach cause no AddRange for observable collection
            foreach (var car in cars) Cars.Add(car);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get cars: {ex.Message}");  // sink is Output Window, when in Debug Mode
            await ShowAlert("Failed to retrieve list of cars");  // could abstract that away and user via DI
        }
        finally
        {
            IsLoading = false;
            IsRefreshing = false;
        }
    }


    [RelayCommand]
    async Task GetCarDetails(int id)
    {
        if (id == 0)
        {
            await ShowAlert("Not details for this car");
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
    }


    [RelayCommand]
    async Task SaveCar()
    {
        if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
        {
            await ShowAlert("Please insert valid data to all fields");
            return;
        }

        var car = new Car
        {
            Id = CarId,
            Make = Make,
            Model = Model,
            Vin = Vin
        };
        if (CarId != 0)
        {
            if (accessType == NetworkAccess.Internet)
            {
                await carApiService.UpdateCar(CarId, car);
                message = carApiService.StatusMessage;
            }
            else
            {
                App.CarService.UpdateCar(car);
                message = carApiService.StatusMessage;
            }
        }
        else
        {
            if (accessType == NetworkAccess.Internet)
            {
                await carApiService.AddCar(car);
                message = carApiService.StatusMessage;
            }
            else
            {
                App.CarService.AddCar(car);
                message = carApiService.StatusMessage;
            }
        }

        await ShowAlert(message);
        await GetCarListAsync();
        await ClearForm();
    }


    [RelayCommand]
    async Task DeleteCar(int id)
    {
        if (id == 0)
        {
            await Shell.Current.DisplayAlert("Invalid Data", "Id not found", "Ok");
            return;
        }

        // in DEBUG mode Shell.Current.DisplayAlert at this point crashes the app!??
        bool confirm = await Shell.Current.DisplayAlert(
            "Confirm Delete",
            "Are you sure you want to delete this entry?",
            "Yes",
            "No");

        if (confirm)
        {
            if (accessType == NetworkAccess.Internet)
            {
                await carApiService.DeleteCar(id);
                message = carApiService.StatusMessage;
            }
            else
            {
                App.CarService.DeleteCar(id);
                message = carApiService.StatusMessage;
            }

            await ShowAlert(message);
            await GetCarListAsync();
        }
    }

    [RelayCommand]
    async Task SetEditMode(int id)
    {
        AddEditButtonText = editButtonText;
        AddEditButtonColor = Color.FromArgb("#ffcc66");
        CarId = id;  // set input id to binding context of the form
        var car = await carApiService.GetCar(id);
        Make = car.Make;
        Model = car.Model;
        Vin = car.Vin;
    }

    [RelayCommand]
    public Task ClearForm()
    {
        AddEditButtonText = addButtonText;
        AddEditButtonColor = null;
        CarId = 0;
        Make = Model = Vin = string.Empty;
        return Task.CompletedTask;
    }

    public async Task ShowAlert(string message)
    {
        await Shell.Current.DisplayAlert("Info", message, "Ok");
    }
}
