using CommunityToolkit.Mvvm.ComponentModel;
using maui_car_list.Models;
using System.Web;

namespace maui_car_list.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]
    Car car;

    [ObservableProperty]
    int id;


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
        Car = App.CarService.GetCar(Id);
    }
}
