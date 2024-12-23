using maui_car_list.ViewModels;

namespace maui_car_list;

// TODO: make global styling where ADD, DELETE, EDIT buttons are defined (e.g. color)
public partial class MainPage : ContentPage
{
    private readonly CarListViewModel _carListViewModel;

    public MainPage(CarListViewModel carListViewModel)
    {
        InitializeComponent();
        _carListViewModel = carListViewModel;
        BindingContext = carListViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        //App.CarService.GetCars();
        //_carListViewModel.GetCarListAsync().Wait();
    }
}
