namespace RitegeDomain.QueryHandlers.UtilisateurQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.UtilisateurQueries;

public class GetOneByNumAccessCardQueryHandler : IRequestHandler<GetOneByNumAccessCardQuery, Utilisateur>
{
    private readonly IUtilisateurRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByNumAccessCardQueryHandler(IUtilisateurRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Utilisateur> Handle(GetOneByNumAccessCardQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByNumAccessCardAsync(request.Number);
        return _mapper.Map<Utilisateur>(entities);
    }
}
