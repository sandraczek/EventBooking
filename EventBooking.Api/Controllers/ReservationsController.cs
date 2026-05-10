using EventBooking.Application.Reservations.Commands.CreateReservation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Api.Controllers;

[ApiController] 
[Route("api/[controller]")] 
public class ReservationsController(IMediator mediator) : ControllerBase
{
    [HttpPost("request")]
    public async Task<IActionResult> ReservationRequest([FromBody] CreateReservationCommand command, CancellationToken cancellationToken)
    {
        var eventId = await mediator.Send(command, cancellationToken);
        
        return Ok(new { Id = eventId, Message = " Request submitted." });
    }
}