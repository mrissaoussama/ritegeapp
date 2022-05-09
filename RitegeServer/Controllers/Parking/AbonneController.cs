using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RitegeDomain.Database;
using RitegeDomain.Database.Queries.Parking.AbonneQueries;
using RitegeDomain.DTO;
using RitegeDomain.QueryHandlers.AbonneQueryHandlers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RitegeServer.ServerControllers
{
    [Authorize]
    [ApiController]
    [Route("Abonne")]
    public class AbonneController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AbonneController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetAll")]
        public async Task<ActionResult<List<Abonne>>> GetAll()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity.Claims;

            foreach (var claim in claims)
            {
                Console.WriteLine(claim.Type, claim.Value);
            }

            var jti = claims.First(claim => claim.Type == "IdUtilisateur").Value;
            var jt2 = claims.First(claim => claim.Type == "Login").Value;
            var query = new GetAllQuery();
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetOneById")]
        public async Task<ActionResult<List<Abonne>>> GetOneByIdAsync(long id)
        {

            var query = new GetOneByIdQuery
            {
                Id = id
            };
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

