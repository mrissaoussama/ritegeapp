namespace RitegeDomain.QueryHandlers.EvenementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Queries.Parking.EvenementQueries;

public class GetAllByDateQueryHandler : IRequestHandler<GetAllByDateQuery, IEnumerable<Evenement>>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByDateQueryHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Evenement>> Handle(GetAllByDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByDateAsync(request.Date, request.AlertsOnly);
        return _mapper.Map<IEnumerable<Evenement>>(entities);
    }
}
