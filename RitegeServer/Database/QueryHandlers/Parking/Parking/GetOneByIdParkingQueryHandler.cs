global using RitegeDomain.Database;
global using RitegeDomain.Database.Entities.Parking;
global using RitegeDomain.Database.IRepositories;
global using RitegeDomain.Model;
using AutoMapper;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.ParkingQueries;

namespace RitegeDomain.QueryHandlers.ParkingDBQueryHandlers.ParkingQueryHandlers;

public class GetOneByIdParkingQueryHandler : IRequestHandler<GetOneByIdParkingQuery,Parking>
{
    private readonly IParkingRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdParkingQueryHandler(IParkingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Parking> Handle(GetOneByIdParkingQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdParkingAsync(request.IdParking);
        return _mapper.Map<Parking>(entities);
    }
}
