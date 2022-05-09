namespace RitegeDomain.QueryHandlers.EvenementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.EvenementQueries;

public class GetAllByBorneAndDateQueryHandler : IRequestHandler<GetAllByBorneAndDateQuery, IEnumerable<Evenement>>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByBorneAndDateQueryHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Evenement>> Handle(GetAllByBorneAndDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByBorneAndDateAsync(request.Id, request.Date,request.AlertsOnly);
        return _mapper.Map<IEnumerable<Evenement>>(entities);
    }
}
