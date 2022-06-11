
using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries;
using RitegeDomain.DTO;

public class DashboardDTOQueryHandler : IRequestHandler<DashboardDTOQuery, DashBoardDTO>
{
    private readonly IDashboardDTORepository _repository;
    private readonly IMapper _mapper;

    public DashboardDTOQueryHandler(IDashboardDTORepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<DashBoardDTO> Handle(DashboardDTOQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetByIdParkingAndIdCashRegister(request.IdParking, request.IdCaisse);
        return _mapper.Map<DashBoardDTO>(entities);
    }
}
