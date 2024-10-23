using maui_car_list.Services;

namespace maui_car_list;

public partial class App : Application
{
    public static CarService CarService { get; private set; }

    public App(CarService carService)
    {
        InitializeComponent();

        MainPage = new AppShell();
        CarService = carService;
    }
}
