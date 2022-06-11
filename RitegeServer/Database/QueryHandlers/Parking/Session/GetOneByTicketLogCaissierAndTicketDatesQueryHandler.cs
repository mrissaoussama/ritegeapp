global using RitegeDomain.Database;
global using RitegeDomain.Database.Entities.Parking;
global using RitegeDomain.Database.IRepositories;
global using RitegeDomain.Model;

using RitegeDomain.Database.Entities.ParkingEntities;

using AutoMapper;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.SessionQueries;

namespace RitegeDomain.QueryHandlers.ParkingDBQueryHandlers;

public class GetOneByTicketLogCaissierAndTicketDatesHandler : IRequestHandler<GetOneByTicketLogCaissierAndTicketDates, Session>
{
    private readonly ISessionRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByTicketLogCaissierAndTicketDatesHandler(ISessionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Session> Handle(GetOneByTicketLogCaissierAndTicketDates request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByTicketLogCaissierAndTicketDatesAsync(request.LogCaissier, request.DateStart,request.DateEnd);
        return _mapper.Map<Session>(entities);
    }
}
