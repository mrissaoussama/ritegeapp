namespace RitegeDomain.QueryHandlers.EvenementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetTodayAbonneQueryHandler : IRequestHandler<GetTodayAbonneQuery,int>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public GetTodayAbonneQueryHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<int> Handle(GetTodayAbonneQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetTodayAbonneAsync(request.IdParking, request.IdCaisse);
        return _mapper.Map<int>(entities);
    }
}
