using EventBooking.Application.Events.Commands.CreateEvent;
using EventBooking.Application.Events.Queries.GetEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Api.Controllers;

[ApiController] 
[Route("api/[controller]")] 
public class EventsController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Register([FromBody] CreateEventCommand command, CancellationToken cancellationToken)
    {
        var eventId = await mediator.Send(command, cancellationToken);
        
        return Ok(new { Id = eventId, Message = "Event created successfully!" });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var events = await mediator.Send(new GetEventsQuery(), cancellationToken);
        return Ok(events);
    }
}