namespace RitegeDomain.CommandHandlers.EventCommandHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Commands.EventCommands;
using RitegeDomain.Database.Queries.ControleAccess.EventQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, int>
{
    private readonly IEventRepository _repository;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IEventRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
  

    public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken) {
        return await _repository.AddAsync(request.DateEvent,request.HeureEvent, request.DoorNumber, request.UserNumber, request.CodeEvent,request.CodeController,request.IndiceController,request.Selected, request.Flux);
    }
}
