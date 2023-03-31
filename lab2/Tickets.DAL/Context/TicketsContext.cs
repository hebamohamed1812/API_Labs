using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Tickets.DAL;

public class TicketsContext : DbContext
{
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Developer> Developers => Set<Developer>();
    public DbSet<Department> Departments => Set<Department>();

    public TicketsContext(DbContextOptions<TicketsContext> options)
        : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var tickets = JsonSerializer.Deserialize<List<Ticket>>("""[{"Id":1,"Description":"Jessie","Severity":0,"EstimationCost":27064,"DepartmentId":1},{"Id":2,"Description":"Jessie","Severity":0,"EstimationCost":27064,"DepartmentId":2},{"Id":3,"Description":"Jessie","Severity":1,"EstimationCost":27064,"DepartmentId":3},{"Id":4,"Description":"Jessie","Severity":1,"EstimationCost":27064,"DepartmentId":5}]""") ?? new();
        var developers = JsonSerializer.Deserialize<List<Developer>>("""[{"Id":1,"Name":"Dana"},{"Id":2,"Name":"Isaac"},{"Id":3,"Name":"Damon"},{"Id":4,"Name":"Miriam"},{"Id":5,"Name":"Terence"},{"Id":6,"Name":"Roosevelt"},{"Id":7,"Name":"Eduardo"},{"Id":8,"Name":"Wilbert"},{"Id":9,"Name":"Tasha"}]""") ?? new();
        var departments = JsonSerializer.Deserialize<List<Department>>("""[{"Id":1,"Name":"Diabetes"},{"Id":2,"Name":"Hypertension"},{"Id":3,"Name":"Asthma"},{"Id":4,"Name":"Depression"},{"Id":5,"Name":"Arthritis"},{"Id":6,"Name":"Allergy"},{"Id":7,"Name":"Flu"}]""") ?? new();

        modelBuilder.Entity<Ticket>().HasData(tickets);
        modelBuilder.Entity<Developer>().HasData(developers);
        modelBuilder.Entity<Department>().HasData(departments);
    }
}
