using Tickets.BL.Dtos;
using Tickets.DAL;

namespace Tickets.BL;

public class TicketsManager : ITicketsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public TicketsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<TicketReadDto> GetAll()
    {
        List<Ticket> ticketsFromDb = _unitOfWork.TicketsRepo.GetAll();
        
        return ticketsFromDb
            .Select(d => new TicketReadDto
            {
                Id = d.Id,
                Severity = d.Severity,
                Description = d.Description
            })
            .ToList();
    }

    public int Add(TicketAddDto ticketDto)
    {
        var ticket = new Ticket
        {
            DepartmentId = ticketDto.DepartmentId,
            Description = ticketDto.Description,
            EstimationCost = ticketDto.EstimationCost
        };
        
        _unitOfWork.TicketsRepo.Add(ticket);
        _unitOfWork.TicketsRepo.SaveChanges();

        return ticket.Id;
    }
}
