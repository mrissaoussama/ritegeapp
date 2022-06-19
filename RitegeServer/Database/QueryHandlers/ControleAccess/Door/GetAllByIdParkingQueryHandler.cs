namespace RitegeDomain.QueryHandlers.Door;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.DoorQueries;

public class GetAllByIdParkingQueryHandler : IRequestHandler<GetAllByIdParkingQuery, IEnumerable<Door>>
{
    private readonly IDoorRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdParkingQueryHandler(IDoorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Door>> Handle(GetAllByIdParkingQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdParkingAsync(request.IdParking);
        return _mapper.Map<IEnumerable<Door>>(entities);
    }
}
