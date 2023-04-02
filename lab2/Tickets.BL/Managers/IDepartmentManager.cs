namespace Tickets.BL.Dtos;

public interface IDepartmentsManager
{
    DepartmentWithTicketsReadDto? GetByIdWithTickets(int id);
}
