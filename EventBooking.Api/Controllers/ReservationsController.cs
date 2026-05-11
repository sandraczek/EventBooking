using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EventBooking.Application.Reservations.Commands.CreateReservation;
using EventBooking.Application.Reservations.Requests.CreateReservation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Api.Controllers;

[ApiController] 
[Route("api/[controller]")] 
[Authorize]
public class ReservationsController(IMediator mediator) : ControllerBase
{
    [HttpPost("request")]
    public async Task<IActionResult> ReservationRequest([FromBody] CreateReservationRequest request, CancellationToken cancellationToken)
    {
        var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub) 
                           ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var studentGuid))
        {
            return Unauthorized("Token invalid. Missing user Id.");
        }
        
        var command = new CreateReservationCommand(request.EventId, studentGuid);
        
        var eventId = await mediator.Send(command, cancellationToken);
        
        return Ok(new { Id = eventId, Message = " Request submitted." });
    }
}