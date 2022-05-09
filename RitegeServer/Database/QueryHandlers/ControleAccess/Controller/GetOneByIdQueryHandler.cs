
global using AutoMapper;
global using MediatR;
global using RitegeDomain.Database.Entities.ControleAccess;
global using System.Threading;
using RitegeDomain.Database.Queries.ControleAccess.ControllerQueries;

namespace RitegeDomain.QueryHandlers.Controller;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, RitegeDomain.Database.Entities.ControleAccess.Controller>
{
    private readonly IControllerRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IControllerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<RitegeDomain.Database.Entities.ControleAccess.Controller> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<RitegeDomain.Database.Entities.ControleAccess.Controller>(entities);
    }
}
