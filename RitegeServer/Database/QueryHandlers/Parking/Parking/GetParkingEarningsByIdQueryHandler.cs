global using RitegeDomain.Database;
global using RitegeDomain.Database.Entities.Parking;
global using RitegeDomain.Database.IRepositories;
global using RitegeDomain.Model;
using AutoMapper;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.ParkingQueries;

namespace RitegeDomain.QueryHandlers.ParkingDBQueryHandlers.ParkingQueryHandlers;

public class GetParkingEarningsByIdQueryHandler : IRequestHandler<GetParkingEarningsByIdQuery, decimal>
{
    private readonly IParkingRepository _repository;
    private readonly IMapper _mapper;

    public GetParkingEarningsByIdQueryHandler(IParkingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<decimal> Handle(GetParkingEarningsByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetParkingEarningsByIdAsync(request.IdParking);
        return _mapper.Map<decimal>(entities);
    }
}
