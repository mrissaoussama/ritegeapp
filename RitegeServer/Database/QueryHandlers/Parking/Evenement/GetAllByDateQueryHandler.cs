﻿namespace RitegeDomain.QueryHandlers.EvenementQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Queries.ParkingDBQueries.EvenementQueries;
using RitegeDomain.Database.Entities.ParkingEntities;

public class GetAllByDateQueryHandler : IRequestHandler<GetAllByDateQuery, IEnumerable<Evenement>>
{
    private readonly IEvenementRepository _repository;
    private readonly IMapper _mapper;

    public GetAllByDateQueryHandler(IEvenementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Evenement>> Handle(GetAllByDateQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByDateAsync(request.DateStart, request.AlertsOnly);
        return _mapper.Map<IEnumerable<Evenement>>(entities);
    }
}
