using MediatR;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.Positions;

namespace StaffManagement.Application.Positions.Commands;

public record UpdatePositionCommand(Guid Id, string Title, byte Grade) : IRequest;

internal class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand>
{
    private readonly IPositionRepository _positionRepository;

    public UpdatePositionCommandHandler(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _positionRepository.Get(request.Id);

        if (position is null)
        {
            throw new NotFoundException("Position not found");
        }

        position.Update(request.Title, request.Grade);

        await _positionRepository.Update(position);
    }
}