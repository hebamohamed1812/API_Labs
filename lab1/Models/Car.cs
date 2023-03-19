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
}