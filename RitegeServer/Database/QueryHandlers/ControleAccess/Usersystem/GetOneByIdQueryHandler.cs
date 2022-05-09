namespace RitegeDomain.QueryHandlers.Usersystem;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Entities.ControleAccess;
using RitegeDomain.Database.Queries.ControleAccess.UsersystemQueries;

public class GetOneByIdQueryHandler : IRequestHandler<GetOneByIdQuery, Usersystem>
{
    private readonly IUsersystemRepository _repository;
    private readonly IMapper _mapper;

    public GetOneByIdQueryHandler(IUsersystemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Usersystem> Handle(GetOneByIdQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetOneByIdAsync(request.Id);
        return _mapper.Map<Usersystem>(entities);
    }
}
