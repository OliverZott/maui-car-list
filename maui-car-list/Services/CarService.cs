using maui_car_list.Models;

namespace maui_car_list.Services;
public class CarService
{
    public List<Car> GetCars()
    {
        var cars = new List<Car>
        {
            new() {Id = 1, Make = "Volvo", Model = "XC90", Vin = "123"},
            new() {Id = 2, Make = "Audi", Model = "A8", Vin = "123"},
            new() {Id = 3, Make = "Nissan", Model = "Impreza", Vin = "123"},
            new() {Id = 4, Make = "BMW", Model = "M3", Vin = "123"},
            new() {Id = 5, Make = "Nissan", Model = "Note", Vin = "123"},
            new() {Id = 5, Make = "Toyota", Model = "Prado", Vin = "123"},
            new() {Id = 5, Make = "Honda", Model = "Fit", Vin = "123"},
        };

        return cars;
    }
}
