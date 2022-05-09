namespace RitegeDomain.QueryHandlers.UtilisateurQueryHandlers;

using AutoMapper;
using MediatR;
using RitegeDomain.Database.Queries.Parking.UtilisateurQueries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, string?>
{
    private readonly IUtilisateurRepository _repository;
    private readonly IMapper _mapper;

    public LoginQueryHandler(IUtilisateurRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<string?> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.Login(request.Login, request.MotDePasse);
        return _mapper.Map<string?>(entities);
    }
}
