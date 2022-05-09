﻿namespace RitegeDomain.QueryHandlers.InfoSessionsDTOQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.InfoSessionsDTOQueries;
using RitegeDomain.DTO;

public class GetAllByNameAndDatesQueryHandler : IRequestHandler<InfoSessionsDTOQuery, IEnumerable<InfoSessionsDTO>>
{
    private readonly IInfoSessionsDTORepository _repository;
    private readonly IMapper _mapper;

    public GetAllByNameAndDatesQueryHandler(IInfoSessionsDTORepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<InfoSessionsDTO>> Handle(InfoSessionsDTOQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllByNameAndDatesAsync(request.Name, request.StartDate, request.FinishDate);
        return _mapper.Map<IEnumerable<InfoSessionsDTO>>(entities);
    }
}