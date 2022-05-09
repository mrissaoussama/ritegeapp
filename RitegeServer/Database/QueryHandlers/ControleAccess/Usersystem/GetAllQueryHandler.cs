namespace RitegeDomain.QueryHandlers.Usersystem;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.UsersystemQueries;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Usersystem>>
{
    private readonly IUsersystemRepository _repository;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(IUsersystemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Usersystem>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<Usersystem>>(entities);
    }
}
