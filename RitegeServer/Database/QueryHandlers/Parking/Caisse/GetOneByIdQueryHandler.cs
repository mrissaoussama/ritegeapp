namespace RitegeDomain.QueryHandlers.CaisseQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.CaisseQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Caisse>
{
    private readonly ICaisseRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(ICaisseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Caisse>Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<Caisse>(entities);
    }
}
