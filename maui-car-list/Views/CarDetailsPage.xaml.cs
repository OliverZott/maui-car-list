using maui_car_list.ViewModels;
namespace maui_car_list.Views;

public partial class CarDetailsPage : ContentPage
{
    public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
    {
        InitializeComponent();
        BindingContext = carDetailsViewModel;  // now CarDetailsPage knows it gets everything from this viewmodel
    }
}