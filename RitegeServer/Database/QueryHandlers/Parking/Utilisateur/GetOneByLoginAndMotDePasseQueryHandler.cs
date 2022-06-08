namespace RitegeDomain.QueryHandlers.UtilisateurQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ParkingEntities;

using RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries;

public class GetOneByLoginAndMotDePasseQueryHandler : IRequestHandler<GetOneByLoginAndMotDePasseQuery, Utilisateur>
{
    private readonly IUtilisateurRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByLoginAndMotDePasseQueryHandler(IUtilisateurRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Utilisateur> Handle(GetOneByLoginAndMotDePasseQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByLoginAndMotDePasseAsync(request.Login, request.MotDePasse);
        return _mapper.Map<Utilisateur>(entities);
    }
}
