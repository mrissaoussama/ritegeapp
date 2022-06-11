namespace RitegeServer.Database.QueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries;
using RitegeDomain.DTO;

public class InfoSessionsDTOQueryHandler : IRequestHandler<InfoSessionsDTOQuery, IEnumerable<InfoSessionsDTO>>
{
    private readonly IInfoSessionsDTORepository _repository;
    private readonly IMapper _mapper;

    public InfoSessionsDTOQueryHandler(IInfoSessionsDTORepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<InfoSessionsDTO>> Handle(InfoSessionsDTOQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByNameAndDatesAsync(request.idCaissier, request.StartDate, request.FinishDate);
        return _mapper.Map<IEnumerable<InfoSessionsDTO>>(entities);
    }
}
