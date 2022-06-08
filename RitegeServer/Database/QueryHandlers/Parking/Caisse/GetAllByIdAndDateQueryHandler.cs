global using RitegeDomain.Database;
global using RitegeDomain.Database.Entities.Parking;
global using RitegeDomain.Database.IRepositories;
global using RitegeDomain.Model;
using AutoMapper;
using RitegeDomain.Database.Entities.ParkingEntities;


using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.CaisseQueries;

namespace RitegeDomain.QueryHandlers.ParkingDBQueryHandlers.CaisseQueryHandlers;

public class GetAllByIdParkingQueryHandler : IRequestHandler<GetAllByIdParkingQuery, IEnumerable<Caisse>>
{
    private readonly ICaisseRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdParkingQueryHandler(ICaisseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Caisse>> Handle(GetAllByIdParkingQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdParkingAsync(request.Id);
        return _mapper.Map<IEnumerable<Caisse>>(entities);
    }
}
