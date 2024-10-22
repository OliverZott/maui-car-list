
using maui_car_list.ViewModels;

namespace maui_car_list;

public partial class MainPage : ContentPage
{
    int count = 0;

    public const double FontSize = 10;
    private readonly CarListViewModel carListViewModel;

    public MainPage(CarListViewModel carListViewModel)
    {
        InitializeComponent();
        BindingContext = carListViewModel;
    }

}


public class GlobalFontSizeExtension : IMarkupExtension
{
    public object ProvideValue(IServiceProvider serviceProvider)
    {
        return MainPage.FontSize;
    }
}
