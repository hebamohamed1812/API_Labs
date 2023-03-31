using Tickets.BL;
using Microsoft.AspNetCore.Mvc;
using Tickets.BL.Dtos;

namespace Tickets.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketsManager _ticketsManager;

    public TicketsController(ITicketsManager ticketsManager)
    {
        _ticketsManager = ticketsManager;
    }

    [HttpGet]
    public ActionResult<List<TicketReadDto>> GetAll()
    {
        return _ticketsManager.GetAll();
    }

    [HttpPost]
    public ActionResult Add(TicketAddDto ticketDto)
    {
        _ticketsManager.Add(ticketDto);
        return NoContent();
    }
}
