namespace RitegeDomain.QueryHandlers.AbonneQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.AbonneQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Abonne>
{
    private readonly IAbonneRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IAbonneRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Abonne> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<Abonne>(entities);
    }
}
