using maui_car_list.ViewModels;
namespace maui_car_list.Views;

public partial class CarEditPage : ContentPage
{
    public CarEditPage()
    {
        InitializeComponent();
    }

    public CarEditPage(CarEditViewModel carEditViewModel)
    {
        InitializeComponent();
        BindingContext = carEditViewModel;
    }


}