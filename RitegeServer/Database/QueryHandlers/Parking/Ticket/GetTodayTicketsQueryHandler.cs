namespace RitegeDomain.QueryHandlers.TicketQueryHandlers;

using AutoMapper;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Entities.ParkingEntities;


using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.TicketQueries;

public class GetTodayTicketsQueryHandler : IRequestHandler<GetTodayTicketsQuery, int>
{
    private readonly ITicketRepository _repository;
    private readonly IMapper _mapper;

    public GetTodayTicketsQueryHandler(ITicketRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<int> Handle(GetTodayTicketsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetTodaysTicketsAsync(request.IdParking,request.IdCaisse);
        return _mapper.Map<int>(entities);
    }
}
