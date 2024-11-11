using MediatR;
using Microsoft.AspNetCore.Mvc;
using person_api_1.Commands;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddPerson([FromBody] AddPersonCommand command)
    {
        try
        {
            var person = await _mediator.Send(command);
            return Ok(person);
        }
        catch (ArgumentException ex)
        {
            // Return a 400 Bad Request with the validation error message
            return BadRequest(new { Error = ex.Message });
        }
    }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetPersonById(Guid id)
    // {
    //     // Assume GetPersonByIdQuery exists
    //     var person = await _mediator.Send(new GetPersonByIdQuery(id));
    //     return person != null ? Ok(person) : NotFound();
    // }
}