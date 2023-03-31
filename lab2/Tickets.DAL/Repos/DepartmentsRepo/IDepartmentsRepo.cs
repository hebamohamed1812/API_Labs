namespace Tickets.DAL;

public interface IDepartmentsRepo: IGenericRepo<Department>
{
    List<Department> GetDepartmentByName(string name);
}
