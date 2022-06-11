namespace RitegeServer.Database.QueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.DTO;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Queries;

public class GetAlertsByIdSocieteAndDateQueryHandler : IRequestHandler<GetAlertsByIdSocieteAndDateQuery, IEnumerable<EventDTO>>
{
    private readonly IEventDTORepository _repository;
    private readonly IMapper _mapper;

    public GetAlertsByIdSocieteAndDateQueryHandler(IEventDTORepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<EventDTO>> Handle(GetAlertsByIdSocieteAndDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAlertsByIdSocieteAndDateAsync(request.IdSociete, request.DateStart, request.DateEnd);
        return _mapper.Map<IEnumerable<EventDTO>>(entities);
    }
}
