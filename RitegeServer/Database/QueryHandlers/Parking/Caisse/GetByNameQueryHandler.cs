namespace RitegeDomain.QueryHandlers.CaisseQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.CaisseQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetByNameQueryHandler : IRequestHandler<GetByNameQuery, Caisse>
{
    private readonly ICaisseRepository _repository;
    private readonly IMapper _mapper;

    public GetByNameQueryHandler(ICaisseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Caisse>Handle(GetByNameQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByNameAsync(request.Name);
        return _mapper.Map<Caisse>(entities);
    }
}
