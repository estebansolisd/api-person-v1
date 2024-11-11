using MediatR;
using Microsoft.AspNetCore.Mvc;
using person_api_1.Commands;
using person_api_1.Models.DTO;
using person_api_1.Queries;

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

    [HttpPost("{id}/record-birth")]
    public async Task<IActionResult> RecordBirth(Guid id, [FromBody] RecordBirthDto dto)
    {
        var command = new RecordBirthCommand(id, dto.BirthDate, dto.BirthLocation);
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonById(Guid id)
    {
        // Assume GetPersonByIdQuery exists
        var person = await _mediator.Send(new GetPersonByIdQuery(id));
        return person != null ? Ok(person) : NotFound();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPersons() =>
        Ok(await _mediator.Send(new GetAllPersonsQuery()));
    
    [HttpGet("{personId}/history")]
    public async Task<IActionResult> GetPersonHistoryAsync(Guid personId)
    {
        var history = await _mediator.Send(new GetPersonHistoryQuery(personId));

        if (history.Count == 0)
        {
            return NotFound($"No history found for person with ID {personId}");
        }

        return Ok(history);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(Guid id, [FromBody] UpdatePersonDto person)
    {
        var success = await _mediator.Send(new UpdatePersonCommand(id, person));

        if (success)
        {
            return Ok();
        }
        
        return NotFound();
    }
}