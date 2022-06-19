namespace RitegeDomain.QueryHandlers.Door;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.DoorQueries;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Door>
{
    private readonly IDoorRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IDoorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Door> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<Door>(entities);
    }
}
