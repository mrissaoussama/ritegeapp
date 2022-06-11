global using RitegeDomain.Database;
global using RitegeDomain.Database.Entities.Parking;
global using RitegeDomain.Database.IRepositories;
global using RitegeDomain.Model;
using AutoMapper;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.ParkingQueries;

namespace RitegeDomain.QueryHandlers.ParkingDBQueryHandlers.ParkingQueryHandlers;

public class GetAllByIdSocieteQueryHandler : IRequestHandler<GetAllByIdSociete, IEnumerable<Parking>>
{
    private readonly IParkingRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdSocieteQueryHandler(IParkingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Parking>> Handle(GetAllByIdSociete request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdSocieteAsync(request.IdSociete);
        return _mapper.Map <IEnumerable <Parking >> (entities);
    }
}
