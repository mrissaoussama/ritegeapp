namespace RitegeDomain.QueryHandlers.ParkingQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Entities.ParkingEntities;


using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.ParkingQueries;

public class GetOneByNomParkingQueryQueryHandler : IRequestHandler<GetOneByNomParkingQuery, Parking >
{
    private readonly IParkingRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByNomParkingQueryQueryHandler(IParkingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Parking>Handle(GetOneByNomParkingQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByNomParkingAsync(request.NomParking);
        return _mapper.Map<Parking>(entities);
    }
}
