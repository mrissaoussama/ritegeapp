namespace RitegeDomain.QueryHandlers.Client;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Entities.ParkingEntities;

using RitegeDomain.Database.Queries.ControleAccess.SocieteQueries;

public class GetOneByNameQueryHandler : IRequestHandler<GetOneByNameQuery, Societe>
{
    private readonly ISocieteRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByNameQueryHandler(ISocieteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Societe> Handle(GetOneByNameQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByNameAsync(request.Name);
        return _mapper.Map<Societe>(entities);
    }
}
