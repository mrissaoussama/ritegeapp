namespace RitegeServer.Database.QueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.DTO;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Queries;

public class GetAllByIdSocieteAndDateQueryHandler : IRequestHandler<GetAllByIdSocieteAndDateQuery, IEnumerable<EventDTO>>
{
    private readonly IEventDTORepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdSocieteAndDateQueryHandler(IEventDTORepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<EventDTO>> Handle(GetAllByIdSocieteAndDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdSocieteAndDateAsync(request.IdSociete, request.DateStart, request.DateEnd);
        return _mapper.Map<IEnumerable<EventDTO>>(entities);
    }
}
