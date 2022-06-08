namespace RitegeDomain.CommandHandlers.EvenementCommandHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Commands.Parking.EvenementCommands;
using RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class CreateEvenementCommandHandler : IRequestHandler<CreateEvenementCommand, int>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public CreateEvenementCommandHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
  

    public async Task<int> Handle(CreateEvenementCommand request, CancellationToken cancellationToken) {
        return await _repository.Add(request.DateEvent,request.DescriptionEvent,request.TypeEvent,request.idCaisse,request.idBorne,request.LogCaissier);
    }
}
