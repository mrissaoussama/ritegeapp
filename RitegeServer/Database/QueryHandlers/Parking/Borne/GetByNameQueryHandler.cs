namespace RitegeDomain.QueryHandlers.BorneQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.BorneQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetByNameQueryHandler : IRequestHandler<GetOneByIdQuery, Borne>
{
    private readonly IBorneRepository _repository;
    private readonly IMapper _mapper;

    public GetByNameQueryHandler(IBorneRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Borne>Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<Borne>(entities);
    }
}
