namespace RitegeDomain.QueryHandlers.Event;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.EventQueries;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Event>
{
    private readonly IEventRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IEventRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Event> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<Event>(entities);
    }
}
