using maui_car_list.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace maui_car_list.Services;

public class CarApiService
{
    HttpClient httpClient;
    public static readonly string baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.100.146:8099" : "http://localhost:8099";
    public string StatusMessage = "";


    public CarApiService()
    {
        httpClient = new() { BaseAddress = new Uri(baseAddress) };
    }


    public async Task<List<Car>> GetCars()
    {
        try
        {
            var response = await httpClient.GetStringAsync("/cars");
            //var response2 = await httpClient.GetAsync("/cars");
            //response2.EnsureSuccessStatusCode();
            //var cars = JsonSerializer.Deserialize<List<Car>>(response);
            var cars = JsonSerializer.Deserialize<List<Car>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return cars;
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data.";
            throw;
        }

    }

    public async Task<Car> GetCar(int id)
    {
        try
        {
            var response = await httpClient.GetStringAsync($"/cars/{id}");
            var car = JsonSerializer.Deserialize<Car>(response);
            return car;
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data.";
            throw;
        }
    }

    public async Task AddCar(Car car)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("/cars", car);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Insert Successful";
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data.";
            throw;
        }
    }

    public async Task UpdateCar(int id, Car car)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"/cars/{id}", car);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Update Successful";
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data.";
            throw;
        }
    }

    public async Task DeleteCar(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"/cars/{id}");
            response.EnsureSuccessStatusCode();
            StatusMessage = "Delete Successful";
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data.";
            throw;
        }
    }
}
