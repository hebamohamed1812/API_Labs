namespace Tickets.DAL;

public class TicketsRepo : GenericRepo<Ticket>, ITicketsRepo
{
    private readonly TicketsContext _context;

    public TicketsRepo(TicketsContext context) : base(context)
    {
        _context = context;
    }

    public List<Ticket> GetTicketsByDepartmentId(int departmentId)
    {
        return _context.Tickets
            .Where(p=>p.DepartmentId == departmentId)
            .ToList();
    }
}
