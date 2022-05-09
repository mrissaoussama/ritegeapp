namespace RitegeDomain.QueryHandlers.AbonneQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.AbonneQueries;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Abonne>>
{
    private readonly IAbonneRepository _repository;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(IAbonneRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Abonne>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<Abonne>>(entities);
    }
}
