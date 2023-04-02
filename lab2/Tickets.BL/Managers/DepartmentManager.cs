using Tickets.BL.Dtos;
using Tickets.DAL;

namespace Tickets.BL;

public class DepartmentsManager : IDepartmentsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public DepartmentWithTicketsReadDto? GetByIdWithTickets(int id)
    {
        Department department = _unitOfWork.DepartmentsRepo.GetByIdWithTickets(id)!;
        if (department is null)
        {
            return null;
        }

        return new DepartmentWithTicketsReadDto
        {
            Id = id,
            Name = department.Name,
            Tickets = department.Tickets.Select(p => new TicketChildReadDto
            {
                Id = p.Id,
                Description = p.Description,
                DevelopersCount = p.Developers.Count
            }).ToList()
        };
    }
}
