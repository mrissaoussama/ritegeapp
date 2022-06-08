namespace RitegeDomain.QueryHandlers.EvenementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetLast10ByBorneQueryHandler : IRequestHandler<GetLast10ByBorneQuery, IEnumerable<Evenement>>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public GetLast10ByBorneQueryHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Evenement>> Handle(GetLast10ByBorneQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetLast10ByBorneAsync(request.Id, request.AlertsOnly);
        return _mapper.Map<IEnumerable<Evenement>>(entities);
    }
}
