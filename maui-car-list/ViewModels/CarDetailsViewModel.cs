using CommunityToolkit.Mvvm.ComponentModel;
using maui_car_list.Models;

namespace maui_car_list.ViewModels;

[QueryProperty(nameof(Car), "Car")]
public partial class CarDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    Car car;

    public CarDetailsViewModel()
    {
        if (car != null) Console.WriteLine(car.Vin);
    }
}
