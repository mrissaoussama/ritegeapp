namespace RitegeDomain.QueryHandlers.Event;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.EventQueries;

public class GetAllByDateAndIdDoorAndEventCodeQueryHandler : IRequestHandler<GetAllByDateAndIdDoorAndEventCodeQuery, IEnumerable<Event>>
{
    private readonly IEventRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByDateAndIdDoorAndEventCodeQueryHandler(IEventRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Event>> Handle(GetAllByDateAndIdDoorAndEventCodeQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByDateAndIdCaisseAndEventCodeAsync(request.Date,request.IdCaisse,request.EventCode);
        return _mapper.Map<IEnumerable<Event>>(entities);
    }
}
