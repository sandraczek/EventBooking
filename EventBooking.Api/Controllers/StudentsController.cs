using EventBooking.Application.Students.Commands.RegisterStudent;
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
}