namespace RitegeDomain.QueryHandlers.AbonnementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.AbonnementQueries;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Abonnement>>
{
    private readonly IAbonnementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(IAbonnementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Abonnement>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<Abonnement>>(entities);
    }
}
