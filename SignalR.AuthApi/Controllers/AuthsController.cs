using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.AuthApi.Code;
using SignalR.Shared.Models;

namespace SignalR.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthsController : ControllerBase
    {
        IConfiguration _configuration;
        public AuthsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Login/{userName}/{password}")]
        public IActionResult Login(string userName, string password)
        {
            TokenHandler tokenHandler = new TokenHandler(_configuration);
            if (userName == "admin" && password == "admin")
            {
                Token token = tokenHandler.CreateAccessToken(5);
                return Ok(token);
            }

            else
            {
                return Ok(new UnauthorizedResult());
            }
        }
    }
}
