namespace RitegeDomain.QueryHandlers.AffectationAbonnementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Queries.ParkingDBQueries.AffectationabonnementQueries;

public class GetAllByAbonnementIdQueryHandler : IRequestHandler<GetAllByAbonnementIdQuery, IEnumerable<Affectationabonnement>>
{
    private readonly IAffectationabonnementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByAbonnementIdQueryHandler(IAffectationabonnementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Affectationabonnement>> Handle(GetAllByAbonnementIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByAbonneIdAsync(request.AbonnementID);
        return _mapper.Map<IEnumerable<Affectationabonnement>>(entities);
    }
}
