using MediatR;

public record UpdatePersonCommand(Guid Id, UpdatePersonDto Person) : IRequest<bool>;
