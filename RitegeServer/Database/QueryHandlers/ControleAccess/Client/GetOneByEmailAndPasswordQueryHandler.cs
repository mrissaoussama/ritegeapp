namespace RitegeDomain.QueryHandlers.Client;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.ClientQueries;

public class GetOneByEmailAndPasswordQueryHandler : IRequestHandler<GetOneByEmailAndPasswordQuery, Client>
{
    private readonly IClientRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByEmailAndPasswordQueryHandler(IClientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Client> Handle(GetOneByEmailAndPasswordQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByEmailAndPasswordAsync(request.Email,request.Password);
        return _mapper.Map<Client>(entities);
    }
}
