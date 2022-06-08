namespace RitegeDomain.QueryHandlers.AffectationAbonnementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.AffectationabonnementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Affectationabonnement>
{
    private readonly IAffectationabonnementRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IAffectationabonnementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Affectationabonnement> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<Affectationabonnement>(entities);
    }
}
