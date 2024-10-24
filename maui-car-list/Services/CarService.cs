using maui_car_list.Models;
using SQLite;

namespace maui_car_list.Services;
public class CarService
{
    private SQLiteConnection connection;
    private string dbPath;
    public string StatusMessage;


    public CarService(string dbPath)
    {
        this.dbPath = dbPath;
    }


    private void Init()
    {
        if (connection != null) return;
        connection = new SQLiteConnection(dbPath);
        connection.CreateTable<Car>();  // wouldn't this always create new table when app starts?
    }

    public List<Car> GetCars()
    {
        try
        {
            Init();
            return connection.Table<Car>().ToList();

        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data.";
            throw;
        }
        finally
        {
        }
    }

    internal Car GetCar(int id)
    {
        try
        {
            Init();
            return connection.Table<Car>().FirstOrDefault(q => q.Id == id);
        }
        catch (Exception)
        {
            StatusMessage = "Failed to retrieve data.";
        }
        return null;
    }

    public void AddCar(Car car)
    {
        // TODO: add validation for uniqueness of Vin!!! where is best place?
        try
        {
            Init();
            if (car == null) throw new Exception("Invalid Car Record");

            var result = connection.Insert(car);
            StatusMessage = result == 0 ? "Insert Failed" : "Insert Successfull";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to insert data.";
        }
    }

    public int DeleteCar(int id)
    {
        int result = 0;
        try
        {
            Init();
            result = connection.Table<Car>().Delete(q => q.Id == id);
            StatusMessage = result == 0 ? "Delete Failed" : "Successfully Deleted";
        }
        catch (Exception)
        {
            StatusMessage = "Failed to delete data.";
        }
        return result;
    }

    public void UpdateCar(Car car)
    {
        if (car == null) throw new Exception("Invalid Car Record");

        try
        {
            Init();
            var result = connection.Update(car);
            StatusMessage = result == 0 ? "Update Failed" : "Successfully Updated";
        }
        catch (Exception)
        {

            StatusMessage = "Failed to update data.";
        }
    }

}
