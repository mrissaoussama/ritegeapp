namespace RitegeDomain.QueryHandlers.AffectationAbonnementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Affectationabonnement>>
{
    private readonly IAffectationabonnementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(IAffectationabonnementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Affectationabonnement>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<Affectationabonnement>>(entities);
    }
}
