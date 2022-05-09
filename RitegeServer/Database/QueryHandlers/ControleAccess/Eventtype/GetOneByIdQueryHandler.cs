namespace RitegeDomain.QueryHandlers.Eventtype;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.EventtypeQueries;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Eventtype>
{
    private readonly IEventtypeRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IEventtypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Eventtype> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<Eventtype>(entities);
    }
}
