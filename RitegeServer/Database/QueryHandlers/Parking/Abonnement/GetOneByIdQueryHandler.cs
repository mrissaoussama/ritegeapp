namespace RitegeDomain.QueryHandlers.AbonnementQueryHandlers;

using AutoMapper;
using RitegeDomain.Database.Entities.ParkingEntities;

using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.AbonnementQueries;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Abonnement>
{
    private readonly IAbonnementRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IAbonnementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Abonnement> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<Abonnement>(entities);
    }
}
