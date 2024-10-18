namespace maui_car_list.Models;

// TODO make should be Model/Table
public class Car : BaseEntity
{
    public string Make { get; set; }
    public string Model { get; set; }
    public string Vin { get; set; }
}
