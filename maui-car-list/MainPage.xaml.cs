using maui_car_list.ViewModels;

namespace maui_car_list;

public partial class MainPage : ContentPage
{

    public MainPage(CarListViewModel carListViewModel)
    {
        InitializeComponent();
        BindingContext = carListViewModel;

        // Storage examples
        // 1: Key-Value pair as preferences in phone:
        // Example for keys: Login ..check if person is logged in
        Preferences.Set("saveDetails", true);
        var detaislSaved = Preferences.Get("saveDetails", false);
    }

}
