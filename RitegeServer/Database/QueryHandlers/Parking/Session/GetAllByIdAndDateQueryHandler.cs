global using RitegeDomain.Database;
global using RitegeDomain.Database.Entities.Parking;
global using RitegeDomain.Database.IRepositories;
global using RitegeDomain.Model;

using RitegeDomain.Database.Entities.ParkingEntities;

using AutoMapper;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.SessionQueries;

namespace RitegeDomain.QueryHandlers.ParkingDBQueryHandlers;

public class GetAllByIdAndDateQueryHandler : IRequestHandler<GetAllByIdAndDateQuery, IEnumerable<Session>>
{
    private readonly ISessionRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdAndDateQueryHandler(ISessionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Session>> Handle(GetAllByIdAndDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdAndDateAsync(request.Id, request.Start, request.End);
        return _mapper.Map<IEnumerable<Session>>(entities);
    }
}
