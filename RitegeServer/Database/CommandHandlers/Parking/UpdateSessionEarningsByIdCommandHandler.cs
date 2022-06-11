namespace RitegeDomain.CommandHandlers.TicketCommandHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Commands.Parking.TicketCommands;
using RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;
using RitegeDomain.Database.Commands.Parking.SessionCommands;

public class UpdateSessionEarningsByIdCommandHandler : IRequestHandler<UpdateSessionEarningsByIdCommand, int>
{
    private readonly ISessionRepository _repository;
    private readonly IMapper _mapper;

    public UpdateSessionEarningsByIdCommandHandler(ISessionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
  

    public async Task<int> Handle(UpdateSessionEarningsByIdCommand request, CancellationToken cancellationToken) {
        return await _repository.UpdateSessionEarningsByIdAsync(request.IdSessions, request.Montant);
    }
}
