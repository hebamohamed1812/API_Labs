using Tickets.DAL;
namespace Tickets.BL.Dtos;

public class TicketReadDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public Severity? Severity { get; set; }
}
