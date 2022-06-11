namespace RitegeDomain.QueryHandlers.Societe;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.SocieteQueries;

public class GetOneByIdDoorQueryHandler : IRequestHandler<GetOneByIdDoorQuery, Societe>
{
    private readonly ISocieteRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdDoorQueryHandler(ISocieteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Societe> Handle(GetOneByIdDoorQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdDoorAsync(request.IdDoor);
        return _mapper.Map<Societe>(entities);
    }
}
