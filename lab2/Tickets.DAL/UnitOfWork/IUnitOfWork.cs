namespace Tickets.DAL;

public interface IUnitOfWork
{
    public ITicketsRepo TicketsRepo { get;  }
    public IDepartmentsRepo DepartmentsRepo { get;  }
}
