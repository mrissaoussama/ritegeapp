namespace RitegeDomain.QueryHandlers.ParkingQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Entities.ParkingEntities;


using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.ParkingQueries;

public class GetAllByIdSocieteQueryHandler : IRequestHandler<GetOneByIdParkingQuery, Parking >
{
    private readonly IParkingRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdSocieteQueryHandler(IParkingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Parking>Handle(GetOneByIdParkingQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdSocieteAsync(request.IdParking);
        return _mapper.Map<Parking>(entities);
    }
}
