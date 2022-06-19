namespace RitegeDomain.QueryHandlers.Door;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.DoorQueries;

public class UpdateDoorStateByIdQueryHandler : IRequestHandler<UpdateDoorStateByIdQuery, int>
{
    private readonly IDoorRepository _repository;
    private readonly IMapper _mapper;

    public UpdateDoorStateByIdQueryHandler(IDoorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateDoorStateByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.UpdateDoorStateByIdAsync(request.IdDoor,request.Activated);
        return _mapper.Map<int>(entities);
    }
}
