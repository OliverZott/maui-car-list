using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_car_list.Models;
using maui_car_list.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace maui_car_list.ViewModels;
public partial class CarListViewModel : BaseViewModel
{
    private const string editButtonText = "Edit Car";
    private const string addButtontext = "Add Car";

    public ObservableCollection<Car> Cars { get; private set; } = [];

    public CarListViewModel()
    {
        Title = "Car List";
        AddEditButtonText = addButtontext;
        AddEditButtonColor = null;
        GetCarListAsync().Wait();  // maybe better with OnApperance in ContentnPage MainPage (see training example)
    }


    [ObservableProperty]
    public bool isRefreshing;
    [ObservableProperty]
    string make;
    [ObservableProperty]
    string model;
    [ObservableProperty]
    string vin;
    [ObservableProperty]
    string addEditButtonText;
    [ObservableProperty]
    int carId;
    [ObservableProperty]
    Color addEditButtonColor;


    [RelayCommand]
    public async Task GetCarListAsync()
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
    async Task GetCarDetails(int id)
    {
        if (id == 0)
        {
            await Shell.Current.DisplayAlert($"Info", "Not details for this car", "Ok");
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
    }


    [RelayCommand]
    async Task SaveCar()
    {
        if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
        {
            await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data to all fields", "Ok");
            return;
        }

        var car = new Car
        {
            Make = Make,
            Model = Model,
            Vin = Vin
        };
        if (CarId != 0)
        {
            car.Id = CarId;
            App.CarService.UpdateCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
        }
        else
        {
            App.CarService.AddCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
        }

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
            var result = App.CarService.DeleteCar(id);
            if (result == 0) await Application.Current.MainPage.DisplayAlert("Error", App.CarService.StatusMessage, "Ok");
            else
            {
                await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");

                // TODO: instead maybe add car to Cars list ....in scope this will trigger refresh also without making new db request
                await GetCarListAsync();
            }
        }
    }


    [RelayCommand]
    async Task SetEditMode(int id)
    {
        AddEditButtonText = editButtonText;
        AddEditButtonColor = Color.FromArgb("#ffcc66");
        CarId = id;  // set input id to binding context of the form
        var car = App.CarService.GetCar(id);
        Make = car.Make;
        Model = car.Model;
        Vin = car.Vin;
    }


    [RelayCommand]
    public Task ClearForm()
    {
        AddEditButtonText = addButtontext;
        AddEditButtonColor = null;
        CarId = 0;
        Make = Model = Vin = string.Empty;
        return Task.CompletedTask;
    }
}
