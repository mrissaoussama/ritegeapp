namespace RitegeDomain.QueryHandlers.SessionQueryHandlers;

using AutoMapper;
using RitegeDomain.Database.Entities.ParkingEntities;

using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.SessionQueries;

public class GetAllByCaisseAndDateQueryHandler : IRequestHandler<GetAllByCaisseAndDateQuery, IEnumerable<Session>>
{
    private readonly ISessionRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByCaisseAndDateQueryHandler(ISessionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Session>> Handle(GetAllByCaisseAndDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByCaisseAndDateAsync(request.Id, request.Start, request.End);
        return _mapper.Map<IEnumerable<Session>>(entities);
    }
}
