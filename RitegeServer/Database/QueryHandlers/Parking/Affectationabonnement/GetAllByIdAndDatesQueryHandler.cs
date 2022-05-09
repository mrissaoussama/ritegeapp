namespace RitegeDomain.QueryHandlers.AffectationAbonnementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.AffectationabonnementQueries;

public class GetAllByIdAndDatesQueryHandler : IRequestHandler<GetAllByIdAndDatesQuery, IEnumerable<Affectationabonnement>>
{
    private readonly IAffectationabonnementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdAndDatesQueryHandler(IAffectationabonnementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Affectationabonnement>> Handle(GetAllByIdAndDatesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdWithDatesAsync(request.Id, request.StartDate, request.FinishDate);
        return _mapper.Map<IEnumerable<Affectationabonnement>>(entities);
    }
}
