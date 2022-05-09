namespace RitegeDomain.QueryHandlers.EvenementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.EvenementQueries;

public class GetAllByCaisseAndBorneAndDateQueryHandler : IRequestHandler<GetAllByCaisseAndBorneAndDateQuery, IEnumerable<Evenement>>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByCaisseAndBorneAndDateQueryHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Evenement>> Handle(GetAllByCaisseAndBorneAndDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByCaisseAndBorneAndDateAsync(request.idCaisse, request.idBorne, request.Date, request.AlertsOnly);
        return _mapper.Map<IEnumerable<Evenement>>(entities);
    }
}
