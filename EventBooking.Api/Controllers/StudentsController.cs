using System.Security.Claims;
using EventBooking.Application.Students.Commands.ConfirmStudentMail;
using EventBooking.Application.Students.Commands.RegisterStudent;
using EventBooking.Application.Students.Commands.SendStudentConfirmationMail;
using EventBooking.Application.Students.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace EventBooking.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")] 
public class StudentsController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterStudentCommand command, CancellationToken cancellationToken)
    {
        var studentId = await mediator.Send(command, cancellationToken);
        
        return Ok(new { Id = studentId, Message = "Student registered successfully!" });
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        try
        {
            var token = await mediator.Send(query);
            return Ok(new { AccessToken = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
    [Authorize(Roles = "Student")]
    [HttpPost("send-confirmation")]
    public async Task<IActionResult> SendConfirmationEmail(CancellationToken cancellationToken)
    {
        var userIdString = User.FindFirstValue(JwtRegisteredClaimNames.Sub) 
                           ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var studentGuid))
        {
            return Unauthorized("Token invalid. Missing student Id.");
        }
        
        var command = new SendStudentConfirmationMailCommand(studentGuid);
        await mediator.Send(command, cancellationToken);

        return Accepted();
    }
    
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            return BadRequest("Bad confirmation data.");

        var result = await mediator.Send(new ConfirmStudentMailCommand(userId, token));

        return result ? 
            Ok(new { Message = "Email confirmed!" }) :
            BadRequest("Could not confirm email.");
    }
}