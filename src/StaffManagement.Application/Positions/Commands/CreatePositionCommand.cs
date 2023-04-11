using MediatR;
using StaffManagement.Domain.Positions;

namespace StaffManagement.Application.Positions.Commands;

public record CreatePositionCommand(string Title, byte Grade) : IRequest;

internal class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand>
{
    private readonly IPositionRepository _positionRepository;

    public CreatePositionCommandHandler(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = Position.CreateNew(request.Title, request.Grade);

        await _positionRepository.Create(position);
    }
}