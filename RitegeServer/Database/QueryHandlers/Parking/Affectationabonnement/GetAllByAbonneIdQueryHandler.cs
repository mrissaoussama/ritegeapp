namespace RitegeDomain.QueryHandlers.AffectationAbonnementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries;

public class GetAllByAbonneIdQueryHandler : IRequestHandler<GetAllByAbonneIdQuery, IEnumerable<Affectationabonnement>>
{
    private readonly IAffectationabonnementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByAbonneIdQueryHandler(IAffectationabonnementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Affectationabonnement>> Handle(GetAllByAbonneIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByAbonneIdAsync(request.AbonneID);
        return _mapper.Map<IEnumerable<Affectationabonnement>>(entities);
    }
}
