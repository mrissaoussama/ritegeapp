namespace RitegeDomain.QueryHandlers.Societe;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.SocieteQueries;

public class GetOneByIdParkingQueryHandler : IRequestHandler<GetOneByIdParkingQuery, Societe>
{
    private readonly ISocieteRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdParkingQueryHandler(ISocieteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Societe> Handle(GetOneByIdParkingQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdParkingAsync(request.IdParking);
        return _mapper.Map<Societe>(entities);
    }
}
