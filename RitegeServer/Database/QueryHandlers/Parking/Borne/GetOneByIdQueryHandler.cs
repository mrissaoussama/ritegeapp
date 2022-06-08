namespace RitegeDomain.QueryHandlers.BorneQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.BorneQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByNameQuery, Borne>
{
    private readonly IBorneRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IBorneRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Borne>Handle(GetOneByNameQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByNameAsync(request.Name);
        return _mapper.Map<Borne>(entities);
    }
}
