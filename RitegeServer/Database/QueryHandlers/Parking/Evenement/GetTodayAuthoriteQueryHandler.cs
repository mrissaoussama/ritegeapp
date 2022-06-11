namespace RitegeDomain.QueryHandlers.EvenementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetTodayAuthoriteQueryHandler : IRequestHandler<GetTodayAutoriteQuery,int>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public GetTodayAuthoriteQueryHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<int> Handle(GetTodayAutoriteQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetTodayAuthoriteAsync(request.IdParking, request.IdCaisse);
        return _mapper.Map<int>(entities);
    }
}
