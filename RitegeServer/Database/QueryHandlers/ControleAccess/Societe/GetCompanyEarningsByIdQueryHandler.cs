namespace RitegeDomain.QueryHandlers.Societe;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.SocieteQueries;

public class GetCompanyEarningsByIdQueryHandler : IRequestHandler<GetCompanyEarningsByIdQuery, decimal>
{
    private readonly ISocieteRepository _repository;
    private readonly IMapper _mapper;

    public GetCompanyEarningsByIdQueryHandler(ISocieteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<decimal> Handle(GetCompanyEarningsByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.IdSociete);
        return _mapper.Map<decimal>(entities);
    }
}
