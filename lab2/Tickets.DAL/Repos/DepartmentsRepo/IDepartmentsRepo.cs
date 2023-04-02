namespace Tickets.DAL;

public interface IDepartmentsRepo: IGenericRepo<Department>
{
    Department? GetByIdWithTickets(int id);
    List<Department> GetDepartmentByName(string name);
}
