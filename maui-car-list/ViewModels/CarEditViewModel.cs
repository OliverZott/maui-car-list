using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_car_list.Models;
using System.Web;

namespace maui_car_list.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CarEditViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] Car car;
    [ObservableProperty] int id;

    [ObservableProperty] public bool isRefreshing;
    [ObservableProperty] string make;
    [ObservableProperty] string model;
    [ObservableProperty] string vin;
    [ObservableProperty] int carId;



    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
        Car = App.CarService.GetCar(Id);
    }


    [RelayCommand]
    async Task GetCarDetails(int id)
    {
        if (id == 0)
        {
            await Shell.Current.DisplayAlert($"Info", "Not details for this car", "Ok");
            return;
        }

        Car = App.CarService.GetCar(Id);
    }


    [RelayCommand]
    async Task EditCar(int id)
    {
        if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
        {
            await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data to all fields", "Ok");
            return;
        }

        var car = App.CarService.GetCar(id);
        car.Make = Make;
        car.Model = Model;
        car.Vin = Vin;

        App.CarService.UpdateCar(car);
        await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");

        await ClearForm();
        Car = App.CarService.GetCar(Id);
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
            if (result == 0)
                await Application.Current.MainPage.DisplayAlert("Error", App.CarService.StatusMessage, "Ok");
            else
            {
                await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");

                // TODO: instead maybe add car to Cars list ....in scope this will trigger refresh also without making new db request
                Car = App.CarService.GetCar(Id);
                ;
            }
        }
    }


    [RelayCommand]
    public Task ClearForm()
    {
        CarId = 0;
        Make = Model = Vin = string.Empty;
        return Task.CompletedTask;
    }
}
