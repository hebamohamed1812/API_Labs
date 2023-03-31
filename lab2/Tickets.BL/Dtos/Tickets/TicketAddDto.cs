namespace Tickets.BL.Dtos;

public class TicketAddDto
{
    public string Description { get; set; } = string.Empty;
    public decimal EstimationCost { get; set; }
    public int DepartmentId { get; set; }
}

