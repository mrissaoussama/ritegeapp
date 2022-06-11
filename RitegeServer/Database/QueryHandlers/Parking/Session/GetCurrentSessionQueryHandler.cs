global using RitegeDomain.Database;
global using RitegeDomain.Database.Entities.Parking;
global using RitegeDomain.Database.IRepositories;
global using RitegeDomain.Model;

using RitegeDomain.Database.Entities.ParkingEntities;

using AutoMapper;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.SessionQueries;

namespace RitegeDomain.QueryHandlers.ParkingDBQueryHandlers;

public class GetCurrentSessionQueryHandler : IRequestHandler<GetCurrentSessionQuery, Session>
{
    private readonly ISessionRepository _repository;
    private readonly IMapper _mapper;

    public GetCurrentSessionQueryHandler(ISessionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Session> Handle(GetCurrentSessionQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetCurrentSessionAsync(request.IdParking, request.IdCaisse);
        return _mapper.Map<Session>(entities);
    }
}
