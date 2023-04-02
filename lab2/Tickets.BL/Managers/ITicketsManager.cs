namespace Tickets.BL.Dtos;

public interface ITicketsManager
{
    List<TicketReadDto> GetAll();
    int Add(TicketAddDto ticket);
}
