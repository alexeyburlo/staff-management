using Dapper;
using MediatR;
using StaffManagement.Application.SharedKernel;
using StaffManagement.Domain.Positions;

namespace StaffManagement.Application.Positions.Queries;

public record GetPositionsCommand : IRequest<List<ShortPositionDto>>;

internal class GetPositionsCommandHandler : IRequestHandler<GetPositionsCommand, List<ShortPositionDto>>
{
    private readonly IConnectionFactory _connectionFactory;

    public GetPositionsCommandHandler(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<ShortPositionDto>> Handle(GetPositionsCommand request, CancellationToken cancellationToken)
    {
        const string query = "SELECT " +
                             $"\"{nameof(Position.Id)}\", " +
                             $"\"{nameof(Position.Title)}\" " +
                             "FROM public.\"positions\"";

        var connection = _connectionFactory.GetOpenConnection();

        var result = await connection.QueryAsync<ShortPositionDto>(query);

        return result.AsList();
    }
}