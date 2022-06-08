namespace RitegeDomain.QueryHandlers.EvenementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Entities.ParkingEntities;

using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;

public class GetAllByCaisseAndDateQueryHandler : IRequestHandler<GetAllByCaisseAndDateQuery, IEnumerable<Evenement>>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByCaisseAndDateQueryHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Evenement>> Handle(GetAllByCaisseAndDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByCaisseAndDateAsync(request.Id, request.Date, request.AlertsOnly);
        return _mapper.Map<IEnumerable<Evenement>>(entities);
    }
}
