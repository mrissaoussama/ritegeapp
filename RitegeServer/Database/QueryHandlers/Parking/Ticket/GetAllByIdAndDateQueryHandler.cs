namespace RitegeDomain.QueryHandlers.TicketQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.TicketQueries;

public class GetAllByIdAndDateQueryHandler : IRequestHandler<GetAllByIdAndDateQuery, IEnumerable<Ticket>>
{
    private readonly ITicketRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByIdAndDateQueryHandler(ITicketRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Ticket>> Handle(GetAllByIdAndDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByIdAndDateAsync(request.Id, request.Start, request.End);
        return _mapper.Map<IEnumerable<Ticket>>(entities);
    }
}
