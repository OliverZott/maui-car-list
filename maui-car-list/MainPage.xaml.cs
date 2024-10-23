using maui_car_list.ViewModels;

namespace maui_car_list;

public partial class MainPage : ContentPage
{

    public MainPage(CarListViewModel carListViewModel)
    {
        InitializeComponent();
        BindingContext = carListViewModel;
    }

}
