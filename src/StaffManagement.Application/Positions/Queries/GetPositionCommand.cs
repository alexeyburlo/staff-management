using Dapper;
using MediatR;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.Positions;

namespace StaffManagement.Application.Positions.Queries;

public record GetPositionCommand(Guid Id) : IRequest<PositionDto>;

internal class GetPositionCommandHandler : IRequestHandler<GetPositionCommand, PositionDto>
{
    private readonly IConnectionFactory _connectionFactory;

    public GetPositionCommandHandler(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<PositionDto> Handle(GetPositionCommand request, CancellationToken cancellationToken)
    {
        const string query = "SELECT " +
                             $"\"{nameof(Position.Id)}\", " +
                             $"\"{nameof(Position.Title)}\", " +
                             $"\"{nameof(Position.Grade)}\" " +
                             "FROM public.\"positions\" " +
                             $"WHERE \"{nameof(Position.Id)}\" = @Id";

        var connection = _connectionFactory.GetOpenConnection();

        var result = await connection.QuerySingleOrDefaultAsync<PositionDto>(query,
            new
            {
                request.Id
            });


        if (result is null)
        {
            throw new NotFoundException("Position not found");
        }

        return result;
    }
}