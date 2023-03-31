using Tickets.DAL;
namespace Tickets.BL;

public class TicketReadDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public Severity? Severity { get; set; }
}
