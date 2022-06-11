namespace RitegeDomain.QueryHandlers.CaisseQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.CaisseQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetTodayEarningsQueryHandler : IRequestHandler<GetTodayEarningsByIdQuery, decimal>
{
    private readonly ICaisseRepository _repository;
    private readonly IMapper _mapper;

    public GetTodayEarningsQueryHandler(ICaisseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<decimal> Handle(GetTodayEarningsByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetTodayEarningsByIdAsync(request.IdCaisse);
        return _mapper.Map<decimal>(entities);
    }
}
