namespace Tickets.BL.Dtos;

public interface ITicketsManager
{
    List<TicketReadDto> GetAll();
    void Add(TicketAddDto ticket);
}
