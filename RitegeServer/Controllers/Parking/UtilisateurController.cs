using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Model;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.UtilisateurQueries;

namespace RitegeServer.ServerControllers
{
    [ApiController]
    [Route("Utilisateur")]
    public class UtilisateurController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UtilisateurController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetOneById")]
        public async Task<ActionResult<List<Utilisateur>>> GetOneByIdAsync(long id)
        {
            var query = new GetOneByIdQuery { Id = id };
            try
            {
                var response = await _mediator.Send(query);
                if (response is not null)
                    return Ok(response);
                else return BadRequest("Bad Credentials");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetOneByLoginAndMotDePasse")]
        public async Task<ActionResult<Utilisateur>> GetOneByLoginAndMotDePasseAsync(string login, string password)
        {
            var query = new GetOneByLoginAndMotDePasseQuery { Login = login, MotDePasse = password };
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Login")]
        public async Task<ActionResult<string?>> Login(LoginQuery loginRequest)
        {
            try
            {
                var response = await _mediator.Send(loginRequest);
                System.Diagnostics.Debug.WriteLine("User Logged in. token: " + response);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetOneByNumAccessCard")]
        public async Task<ActionResult<List<Utilisateur>>> GetOneByNumAccessCardAsync(string number)
        {
            var query = new GetOneByNumAccessCardQuery { Number = number };
            try
            {
                var response = await _mediator.Send(query);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

