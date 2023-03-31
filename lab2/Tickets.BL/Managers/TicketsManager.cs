using Tickets.BL.Dtos;
using Tickets.DAL;

namespace Tickets.BL;

public class TicketsManager : ITicketsManager
{
    private readonly ITicketsRepo _ticketsRepo;

    public TicketsManager(ITicketsRepo ticketsRepo)
    {
        _ticketsRepo = ticketsRepo;
    }

    public List<TicketReadDto> GetAll()
    {
        List<Ticket> ticketsFromDb = _ticketsRepo.GetAll();
        
        return ticketsFromDb
            .Select(d => new TicketReadDto
            {
                Id = d.Id,
                Severity = d.Severity,
                Description = d.Description
            })
            .ToList();
    }

    public void Add(TicketAddDto ticketDto)
    {
        var ticket = new Ticket
        {
            DepartmentId = ticketDto.DepartmentId,
            Description = ticketDto.Description,
            EstimationCost = ticketDto.EstimationCost
        };
        
        _ticketsRepo.Add(ticket);
        _ticketsRepo.SaveChanges();
    }
}
