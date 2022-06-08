global using RitegeDomain.Database;
global using RitegeDomain.Database.Entities.Parking;
global using RitegeDomain.Database.IRepositories;
global using RitegeDomain.Model;
using AutoMapper;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.BorneQueries;

namespace RitegeDomain.QueryHandlers.ParkingDBQueryHandlers.BorneQueryHandlers;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetAllByIdParkingQueryHandler : IRequestHandler<GetAllByIdParkingQuery, IEnumerable<Borne>>
{
    private readonly IBorneRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdParkingQueryHandler(IBorneRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Borne>> Handle(GetAllByIdParkingQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdParkingAsync(request.Id);
        return _mapper.Map<IEnumerable<Borne>>(entities);
    }
}
