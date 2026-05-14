using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EventBooking.Application.Reservations.Commands.CreateReservation;
using EventBooking.Application.Reservations.Queries.GetMyReservations;
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
    [Authorize]
    public async Task<IActionResult> ReservationRequest([FromBody] CreateReservationRequest request, CancellationToken cancellationToken)
    {
        var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub) 
                           ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userGuid))
        {
            return Unauthorized("Token invalid. Missing user Id.");
        }
        
        var command = new CreateReservationCommand(request.EventId, userGuid, User.FindFirstValue(ClaimTypes.Role));
        
        await mediator.Send(command, cancellationToken);
        
        return Ok(new {Message = " Request submitted." });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMy(CancellationToken cancellationToken)
    {
        var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub) 
                           ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userGuid))
        {
            return Unauthorized("Token invalid. Missing user Id.");
        }

        var reservations = await mediator.Send(new GetMyReservationsQuery(userGuid), cancellationToken);

        return Ok(reservations);
    }
        
}