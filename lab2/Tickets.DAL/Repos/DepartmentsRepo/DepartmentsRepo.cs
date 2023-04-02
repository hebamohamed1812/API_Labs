using Microsoft.EntityFrameworkCore;

namespace Tickets.DAL;

public class DepartmentsRepo : GenericRepo<Department>, IDepartmentsRepo
{
    private readonly TicketsContext _context;

    public DepartmentsRepo(TicketsContext context) : base(context)
    {
        _context = context;
    }

    public List<Department> GetDepartmentByName(string name)
    {
        return _context.Departments
            .Where(d => d.Name == name)
            .ToList();
    }

    public Department? GetByIdWithTickets(int id)
    {
        return _context.Departments
            .Include(d => d.Tickets)
                .ThenInclude(p => p.Developers)
            .FirstOrDefault(d => d.Id == id);
    }
}
