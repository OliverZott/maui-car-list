using maui_car_list.ViewModels;

namespace maui_car_list;

// TODO: make global styling where ADD, DELETE, EDIT buttons are defined (e.g. color)
public partial class MainPage : ContentPage
{

    public MainPage(CarListViewModel carListViewModel)
    {
        InitializeComponent();
        BindingContext = carListViewModel;
    }

}
