namespace RitegeDomain.QueryHandlers.Controller;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.ControllerQueries;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Controller>>
{
    private readonly IControllerRepository _repository;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(IControllerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Controller>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<Controller>>(entities);
    }
}
