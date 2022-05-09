namespace RitegeDomain.QueryHandlers.Eventtype;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.EventtypeQueries;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Eventtype>>
{
    private readonly IEventtypeRepository _repository;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(IEventtypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Eventtype>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<Eventtype>>(entities);
    }
}
