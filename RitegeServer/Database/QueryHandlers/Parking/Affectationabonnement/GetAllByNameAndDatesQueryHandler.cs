namespace RitegeDomain.QueryHandlers.AffectationAbonnementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.AffectationabonnementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetAllByNameAndDatesQueryHandler : IRequestHandler<GetAllByNameAndDatesQuery, IEnumerable<Affectationabonnement>>
{
    private readonly IAffectationabonnementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByNameAndDatesQueryHandler(IAffectationabonnementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Affectationabonnement>> Handle(GetAllByNameAndDatesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByNameAndDatesAsync(request.Name, request.StartDate, request.FinishDate);
        return _mapper.Map<IEnumerable<Affectationabonnement>>(entities);
    }
}
