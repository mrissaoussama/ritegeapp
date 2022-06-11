namespace RitegeDomain.QueryHandlers.UtilisateurQueryHandlers;

using AutoMapper;
using RitegeDomain.Database.Entities.ParkingEntities;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries;
public class GetAllByIdSocieteQueryHandler : IRequestHandler<GetAllByIdSocieteQuery, IEnumerable<Utilisateur>>
{
    private readonly IUtilisateurRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdSocieteQueryHandler(IUtilisateurRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Utilisateur>> Handle(GetAllByIdSocieteQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdSocieteAsync(request.IdSociete);
        return _mapper.Map<IEnumerable<Utilisateur>>(entities);
    }
}
