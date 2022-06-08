namespace RitegeDomain.QueryHandlers.UtilisateurQueryHandlers;

using AutoMapper;
using RitegeDomain.Database.Entities.ParkingEntities;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries;
public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Utilisateur>
{
    private readonly IUtilisateurRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IUtilisateurRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Utilisateur> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<Utilisateur>(entities);
    }
}
