namespace RitegeDomain.QueryHandlers.InfoAbonnementDTOQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.InfoAbonnementDTOQueries;
using RitegeDomain.DTO;

public class InfoAbonnementDTOQueryHandler : IRequestHandler<InfoAbonnementDTOQuery, IEnumerable<InfoAbonnementDTO>>
{
    private readonly IInfoAbonnementDTORepository _repository;
    private readonly IMapper _mapper;

    public InfoAbonnementDTOQueryHandler(IInfoAbonnementDTORepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<InfoAbonnementDTO>> Handle(InfoAbonnementDTOQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByNameAndDatesAsync(request.Name, request.StartDate, request.FinishDate);
        return _mapper.Map<IEnumerable<InfoAbonnementDTO>>(entities);
    }
}
