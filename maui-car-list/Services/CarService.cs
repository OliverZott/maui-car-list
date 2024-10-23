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
        connection.CreateTable<Car>();
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
}
