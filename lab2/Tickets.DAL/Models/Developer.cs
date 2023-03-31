namespace Tickets.DAL;

public class Developer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
}