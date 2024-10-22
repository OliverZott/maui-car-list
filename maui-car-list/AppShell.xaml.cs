using maui_car_list.Views;
using maui_car_list.Views.TestPages;

namespace maui_car_list;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(CarDetailsPage), typeof(CarDetailsPage));
        Routing.RegisterRoute(nameof(LayoutExample), typeof(LayoutExample));
        Routing.RegisterRoute(nameof(TestPage), typeof(TestPage));
        Routing.RegisterRoute(nameof(TestPage2), typeof(TestPage2));
    }
}
