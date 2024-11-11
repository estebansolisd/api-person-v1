using MediatR;

namespace person_api_1.Commands;

public record AddPersonCommand(Person Person) : IRequest<Person>;
