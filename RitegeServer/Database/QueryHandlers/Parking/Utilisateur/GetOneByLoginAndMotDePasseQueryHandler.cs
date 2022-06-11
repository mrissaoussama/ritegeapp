namespace RitegeDomain.QueryHandlers.UtilisateurQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ParkingEntities;

using RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries;

public class GetOneByLoginQueryHandler : IRequestHandler<GetOneByLoginQuery, Utilisateur>
{
    private readonly IUtilisateurRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByLoginQueryHandler(IUtilisateurRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Utilisateur> Handle(GetOneByLoginQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByLoginAsync(request.Login);
        return _mapper.Map<Utilisateur>(entities);
    }
}
