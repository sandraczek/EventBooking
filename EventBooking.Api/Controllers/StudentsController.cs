using EventBooking.Application.Students.Commands.RegisterStudent;
using EventBooking.Application.Students.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Api.Controllers;

[ApiController] 
[Route("api/[controller]")] 
public class StudentsController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterStudentCommand command, CancellationToken cancellationToken)
    {
        var studentId = await mediator.Send(command, cancellationToken);
        
        return Ok(new { Id = studentId, Message = "Student registered successfully!" });
    }
    
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
}