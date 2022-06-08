namespace RitegeDomain.QueryHandlers.InfoSessionsDTOQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.ParkingDBQueries.InfoSessionsDTOQueries;
using RitegeDomain.Database.Queries.ParkingDBQueries.InfoTicketDTOQueries;
using RitegeDomain.DTO;

public class InfoTicketDTOQueryHandler : IRequestHandler<InfoTicketDTOQuery, IEnumerable<InfoTicketDTO>>
{
    private readonly IInfoTicketDTORepository _repository;
    private readonly IMapper _mapper;

    public InfoTicketDTOQueryHandler(IInfoTicketDTORepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<InfoTicketDTO>> Handle(InfoTicketDTOQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByDatesAsync(request.StartDate, request.FinishDate);
        return _mapper.Map<IEnumerable<InfoTicketDTO>>(entities);
    }
}
