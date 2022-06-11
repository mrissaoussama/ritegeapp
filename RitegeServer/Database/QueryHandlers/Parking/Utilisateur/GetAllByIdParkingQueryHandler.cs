namespace RitegeDomain.QueryHandlers.UtilisateurQueryHandlers;

using AutoMapper;
using RitegeDomain.Database.Entities.ParkingEntities;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries;
public class GetAllByIdParkingQueryHandler : IRequestHandler<GetAllByIdParkingQuery, IEnumerable<Utilisateur>>
{
    private readonly IUtilisateurRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdParkingQueryHandler(IUtilisateurRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Utilisateur>> Handle(GetAllByIdParkingQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdParkingAsync(request.IdParking);
        return _mapper.Map<IEnumerable<Utilisateur>>(entities);
    }
}
