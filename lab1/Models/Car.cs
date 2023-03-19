using lab1.Validations;
using System.ComponentModel.DataAnnotations;

namespace lab1.Models;
public class Car
{
    public int Id { get; set; }

    [Required]
    public string Model { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    [DateInPast]
    public DateTime ProductionDate { get; set; }
    public double Price { get; set; }

    // public Car(int id, string model, DateTime productionDate, double price, string type)
    // {
    //     Id = id;
    //     Model = model;
    //     ProductionDate = productionDate;
    //     Price = price;
    //     Type = type;
    // }

    // private static List<Car> _cars = new List<Car>();
    // {
    //     new (1, "Samsung", new DateTime(2011, 5, 5), 799.99),
    //     new (2, "Apple", new DateTime(2012, 5, 5), 999.99),
    //     new (3, "OnePlus", new DateTime(2013, 5, 5), 699.99),
    //     new (4, "Huawei", new DateTime(2014, 5, 5), 899.99),
    //     new (5, "Nokia", new DateTime(2015, 5, 5), 499.99)
    // };

    // public static List<Car> GetCars() => _cars;
}