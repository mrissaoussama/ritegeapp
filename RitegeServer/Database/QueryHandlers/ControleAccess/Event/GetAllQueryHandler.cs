namespace RitegeDomain.QueryHandlers.Event;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.EventQueries;

public class GetAllByDateQueryHandler : IRequestHandler<GetAllByDateQuery, IEnumerable<Event>>
{
    private readonly IEventRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByDateQueryHandler(IEventRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Event>> Handle(GetAllByDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByDateAsync(request.Date);
        return _mapper.Map<IEnumerable<Event>>(entities);
    }
}
