namespace RitegeDomain.QueryHandlers.Client;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.ClientQueries;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Client>
{
    private readonly IClientRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IClientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Client> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.IdClient);
        return _mapper.Map<Client>(entities);
    }
}
