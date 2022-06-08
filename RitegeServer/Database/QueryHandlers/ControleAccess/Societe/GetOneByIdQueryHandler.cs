namespace RitegeDomain.QueryHandlers.Societe;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.SocieteQueries;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Societe>
{
    private readonly ISocieteRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(ISocieteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Societe> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.IdSociete);
        return _mapper.Map<Societe>(entities);
    }
}
