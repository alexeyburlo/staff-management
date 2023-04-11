using MediatR;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.Positions;

namespace StaffManagement.Application.Positions.Commands;

public record DeletePositionCommand(Guid Id) : IRequest;

internal class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand>
{
    private readonly IPositionRepository _positionRepository;

    public DeletePositionCommandHandler(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _positionRepository.Get(request.Id);

        if (position is null)
        {
            throw new NotFoundException("Position not found");
        }

        await _positionRepository.Delete(position);
    }
}