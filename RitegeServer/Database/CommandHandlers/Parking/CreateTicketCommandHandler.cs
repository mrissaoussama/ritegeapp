namespace RitegeDomain.CommandHandlers.TicketCommandHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Commands.Parking.TicketCommands;
using RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Ticket>
{
    private readonly ITicketRepository _repository;
    private readonly IMapper _mapper;

    public CreateTicketCommandHandler(ITicketRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
  

    public async Task<Ticket> Handle(CreateTicketCommand request, CancellationToken cancellationToken) {
        return await _repository.Add(request.DateHeureDebutStationnement,request.DateHeureFinStationnement,request.EtatTicket,request.IdTarifTicket,request.Tarif,request.idBorneEntree,request.idBorneSortie,request.LogCaissier,request.AvecTarif2);
    }
}
