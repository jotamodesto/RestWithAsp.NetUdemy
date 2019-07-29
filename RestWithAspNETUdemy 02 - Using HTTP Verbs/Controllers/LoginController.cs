using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNETUdemy.Business;
using RestWithAspNETUdemy.Data.VO;
using RestWithAspNETUdemy.Model;

namespace RestWithAspNETUdemy.Controllers
{
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiVersion("1")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;
        public LoginController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Post([FromBody] User login)
        {
            if (login == null) return BadRequest();
            var userAuthenticated = _loginBusiness.FindByLogin(login);
            if (userAuthenticated == null) return NotFound();
            return Ok(userAuthenticated);
        }
    }
}