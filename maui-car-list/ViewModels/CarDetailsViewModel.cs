﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maui_car_list.Models;
using maui_car_list.Views;
using System.Web;

namespace maui_car_list.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] Car car;
    [ObservableProperty] int id;

    [ObservableProperty] public bool isRefreshing;
    [ObservableProperty] string make;
    [ObservableProperty] string model;
    [ObservableProperty] string vin;
    [ObservableProperty] string addEditButtonText;
    [ObservableProperty] int carId;


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
        Car = App.CarService.GetCar(Id);
    }


    [RelayCommand]
    async Task EditCar(int id)
    {
        if (id == 0)
        {
            await Shell.Current.DisplayAlert($"Info", "Not details for this car", "Ok");
            return;
        }
        // Clear navigation stack to avoid ambiguity and navigate to editPage
        //await Shell.Current.Navigation.PopToRootAsync();
        await Shell.Current.GoToAsync($"{nameof(CarEditPage)}?Id={id}", true);
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

                // Clear navigation stack to avoid ambiguity and navigate back to MainPage
                //await Shell.Current.Navigation.PopToRootAsync();
                //await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

                // above solution throws exception
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
