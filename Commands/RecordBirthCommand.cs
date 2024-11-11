using MediatR;

namespace person_api_1.Commands;

public record RecordBirthCommand(Guid Id, DateTime BirthDate, string BirthLocation) : IRequest<bool>;
