namespace RitegeDomain.QueryHandlers.EvenementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetTodayAdministrateurQueryHandler : IRequestHandler<GetTodayAdministrateurQuery,int>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public GetTodayAdministrateurQueryHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<int> Handle(GetTodayAdministrateurQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetTodayAdministrateurAsync(request.IdParking, request.IdParking);
        return _mapper.Map<int>(entities);
    }
}
