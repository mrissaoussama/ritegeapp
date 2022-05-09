namespace RitegeDomain.QueryHandlers.EvenementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.EvenementQueries;

public class GetLast10QueryHandler : IRequestHandler<GetLast10Query, IEnumerable<Evenement>>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public GetLast10QueryHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Evenement>> Handle(GetLast10Query request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetLast10Async(request.AlertsOnly);
        return _mapper.Map<IEnumerable<Evenement>>(entities);
    }
}
