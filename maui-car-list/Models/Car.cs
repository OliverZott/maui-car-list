using SQLite;

namespace maui_car_list.Models;

// TODO make should be Model/Table
[Table("cars")]
public class Car : BaseEntity
{

    public string Make { get; set; }
    public string Model { get; set; }
    [Unique]
    [MaxLength(12)]
    public string Vin { get; set; }

    public string FullCarName => $"{Make} - {Model}";
}
