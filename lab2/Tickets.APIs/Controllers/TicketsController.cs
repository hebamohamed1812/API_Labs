using Tickets.BL;
using Microsoft.AspNetCore.Mvc;
using Tickets.BL.Dtos;

namespace Tickets.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketsManager _ticketsManager;
    private readonly IDepartmentsManager _departmentsManager;

    public TicketsController(ITicketsManager ticketsManager, IDepartmentsManager departmentsManager)
    {
        _ticketsManager = ticketsManager;
        _departmentsManager = departmentsManager;
    }

    [HttpGet]
    public ActionResult<List<TicketReadDto>> GetAll()
    {
        return _ticketsManager.GetAll();
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<DepartmentWithTicketsReadDto> GetByIdWithTickets(int id)
    {
        var departmentDto = _departmentsManager.GetByIdWithTickets(id);
        if (departmentDto is null)
        {
            return NotFound();
        }
        return departmentDto;
    }

    [HttpPost]
    public ActionResult Add(TicketAddDto ticketDto)
    {
        _ticketsManager.Add(ticketDto);
        return NoContent();
    }
}
